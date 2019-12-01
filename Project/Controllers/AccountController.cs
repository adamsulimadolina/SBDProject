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

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
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
            if (ModelState.IsValid)
            {
                using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
                {
                    db.User.Add(user);//db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "Registration successfull" + user.Login;
                return RedirectToAction("Index", "Home");
            }
            return View();
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
    }
}