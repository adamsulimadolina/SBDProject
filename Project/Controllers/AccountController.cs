using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Project.Data;
using Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        DbContextOptionsBuilder<ProjectContext> _optionsBuilder;
        private readonly ProjectContext _context;


        public AccountController(IConfiguration configuration, ProjectContext context)
        {
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            var users = _context.User.ToList();
            foreach(var elem in users)
            {
                if(elem.Login.Equals(user.Login))
                {
                    ViewBag.Message = "Podany użytkownik już istnieje!";
                    return View();
                }
            }
            if (ModelState.IsValid)
            {
                LogsModel register_log = new LogsModel();
                register_log.Log = "Registration - User: " + user.Login;
                register_log.MessageDate = System.DateTime.Now;
                register_log.UserR = user;
                using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
                {
                    db.User.Add(user);//db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                
                using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
                {
                    var usr = db.User.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
                    register_log.UserID = usr.UserID;
                    db.Logs.Add(register_log);
                    db.SaveChanges();
                }
                ViewBag.Message = "Registration successfull" + user.Login;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
            {
                var usr = db.User.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
                if (usr != null)
                {
                    this.HttpContext.Session.SetString("UserID", usr.UserID.ToString());
                    this.HttpContext.Session.SetString("Username", usr.Login.ToString());
                    LogsModel register_log = new LogsModel();
                    register_log.Log = "Login - User: " + usr.Login;
                    register_log.MessageDate = System.DateTime.Now;
                    register_log.UserR = usr;
                    register_log.UserID = usr.UserID;
                    db.Logs.Add(register_log);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Login lub hasło są niepoprawne.");
                    return View(user);
                }
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LoggedIn()
        {
            if (this.HttpContext.Session.Get("UserID") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            if(this.HttpContext.Session.Get("UserID") != null)
            {
                this.HttpContext.Session.Clear();
                
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details()
        {
            var id = this.HttpContext.Session.GetString("UserID");
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.User
                .Where(m => m.UserID == int.Parse(id))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Where(m => m.UserID.Equals(user.UserID))
                .FirstOrDefaultAsync();

            var tenant = await _context.Tenants
                .Where(m => m.UserID.Equals(user.UserID))
                .FirstOrDefaultAsync();

            if (owner == null) owner = new OwnerModel();
            if (tenant == null) tenant = new TenantModel();
            ViewData["Owner"] = owner;
            ViewData["Tenant"] = tenant;
            return View(user);
        }

        public IActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, [Bind(Prefix = "Item1")] string old_password, 
            [Bind(Prefix = "Item2")] string new_password, [Bind(Prefix = "Item3")] string new_password_repeat)
        {
            var user = await _context.User
                .Where(m => m.UserID.Equals(id))
                .FirstOrDefaultAsync();

            if(!user.Password.Equals(old_password))
            {
                ViewBag.OldPass = "Obecne hasło jest niepoprawne.";
                return View();
            }
            if(new_password != new_password_repeat)
            {
                ViewBag.NewPass = "Nowe hasło oraz jego powtórzenie muszą być takie same!";
                return View();
            }

            user.Password = new_password;
            user.VerifyPassword = new_password_repeat;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            

            var userModel = await _context.User.FindAsync(id);

            var pairsModel = _context.Pairs
                .Include(m => m.Tenant_1)
                .Include(m => m.Tenant_2)
                .Where(m => m.Tenant_1.UserID.Equals(id));
            var pairsModel2 = _context.Pairs
                .Include(m => m.Tenant_1)
                .Include(m => m.Tenant_2)
                .Where(m => m.Tenant_2.UserID.Equals(id));
            foreach(var pair in pairsModel)
            {
                _context.Pairs.Remove(pair);
            }
            foreach(var pair in pairsModel)
            {
                _context.Pairs.Remove(pair);
            }
            _context.User.Remove(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Logout");
        }

    }
}
