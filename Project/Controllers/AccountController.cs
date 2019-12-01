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
                    ModelState.AddModelError("", "Username or Password is wrong.");
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
    }
}