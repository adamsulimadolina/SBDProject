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
using Newtonsoft.Json;
using PusherServer;

namespace Project.Controllers
{
    public class AuthController : Controller
    {
        private Pusher pusher;
        public IActionResult Index()
        {
            return View();
        }
        

        //class constructor
        public AuthController()
        {

            var options = new PusherOptions();
            options.Cluster = "eu";

            pusher = new Pusher(
               "904061",
               "85b88ce58ddae993b77e",
               "db25103a35529ad79414",
               options
           );
        }

        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {
            if (this.HttpContext.Session.Get("UserID") == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }

            UserModel currentUser = new UserModel
            {
                UserID = Convert.ToInt32(this.HttpContext.Session.GetString("UserID")),
                Login = this.HttpContext.Session.GetString("Username")
            };         

            if (channel_name.IndexOf(currentUser.UserID.ToString()) == -1)
            {
                return Json(
                  new { status = "error", message = "User cannot join channel" }
                );
            }

            var auth = pusher.Authenticate(channel_name, socket_id);

            return Json(auth);
        }
    }
}