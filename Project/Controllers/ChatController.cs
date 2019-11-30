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
    public class ChatController : Controller
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        DbContextOptionsBuilder<ProjectContext> _optionsBuilder;
        private Pusher pusher;
        private UserModel SelectedUser;

        public ChatController(IConfiguration configuration)
        {
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
            var options = new PusherOptions();
            options.Cluster = "eu";            
            pusher = new Pusher(
           "904061",
           "85b88ce58ddae993b77e",
           "db25103a35529ad79414", options);
        }
       
        
        public ActionResult OwnersHelp(int? id)
        {
            return RedirectToAction("Index");
        }
        public ActionResult TenatsHelp(int? id)
        {
            if(id!=null)
            {
                using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
                {
                    var query3 = from owner in db.Owners
                                 where owner.OwnerID == id
                                 select owner;
                    var Owner = query3.FirstOrDefault();
                    var query4 = from user in db.User
                                 where user.UserID == Owner.UserID
                                 select user;
                    SelectedUser = query4.FirstOrDefault();
                }
            }
            return RedirectToAction("Index");
        }
      
        public ActionResult Index(int? id)
        {
            
            if (this.HttpContext.Session.Get("UserID") == null)
            {
                return RedirectToAction("Login","Account");
            }

            UserModel currentUser = new UserModel { UserID= Convert.ToInt32(this.HttpContext.Session.GetString("UserID")),
            Login = this.HttpContext.Session.GetString("Username")
        };
            if (id != null)
            {
                using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
                {
                    
                    var query1 = from tenant in db.Tenants
                               where tenant.TenantID == id
                               select tenant;
                    var najemca = query1.FirstOrDefault();
                    var query2 = from user in db.User
                                 where user.UserID == najemca.UserID
                                 select user;
                   
                    ViewBag.selectedUser = query2.FirstOrDefault();
                }
            }
            else
            {
                ViewBag.selectedUser = new UserModel
                {
                    UserID = 0,
                    Login = "0",
                };
            }

            using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
            {

                ViewBag.allUsers = db.User.Where(u => u.UserID != currentUser.UserID)
                                 .ToList();
            }

            ViewBag.currentUser = currentUser;

            return View();
            
        }
        public JsonResult ConversationWithContact(int contact)
        {
            var sa = new JsonSerializerSettings();
            if (this.HttpContext.Session.Get("UserID") == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }
           
                UserModel currentUser = new UserModel
            {
                UserID = Convert.ToInt32(this.HttpContext.Session.GetString("UserID")),
                Login = this.HttpContext.Session.GetString("Username")
            };

            var conversations = new List<MessagesModel>();

            using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
            {
                conversations = db.Messages.
                                  Where(c => (c.UserReceiverID == currentUser.UserID
                                      && c.UserSenderID == contact) ||
                                      (c.UserReceiverID == contact
                                      && c.UserSenderID == currentUser.UserID))
                                  .OrderBy(c => c.MessageDate)
                                  .ToList();
            }

            return Json(
                new { status = "success", data = conversations },sa
                
            );
        }
        [HttpPost]
        public JsonResult SendMessage()
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

            string socket_id = Request.Form["socket_id"];
                    
                MessagesModel convo = new MessagesModel
                {
                    UserSenderID = currentUser.UserID,
                    Message = Request.Form["Message"],
                    UserReceiverID = Convert.ToInt32(Request.Form["contact"])
                };
           
       
            convo.MessageDate = DateTime.Now;
            using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
            {
                db.Messages.Add(convo);
                db.SaveChanges();
            }

            var conversationChannel = getConvoChannel(currentUser.UserID, convo.UserReceiverID);
            pusher.TriggerAsync(
              conversationChannel,
              "new_message",
              convo,
              new TriggerOptions() { SocketId = socket_id });

            return Json(convo);
        }
        [HttpPost]
        //public JsonResult MessageDelivered(int message_id)
      //  {
           /* MessagesModel convo = null;
            using (ProjectContext db = new ProjectContext(_optionsBuilder.Options))
            {
                convo = db.Messages.FirstOrDefault(c => c.id == message_id);
                if (convo != null)
                {
                    convo.status = Conversation.messageStatus.Delivered;
                    db.Entry(convo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            string socket_id = Request.Form["socket_id"];
            var conversationChannel = getConvoChannel(convo.sender_id, convo.receiver_id);
            pusher.TriggerAsync(
              conversationChannel,
              "message_delivered",
              convo,
              new TriggerOptions() { SocketId = socket_id });
            return Json(convo);*/
       // }
        private String getConvoChannel(int user_id, int contact_id)
        {
            if (user_id > contact_id)
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }

            return "private-chat-" + user_id + "-" + contact_id;
        }
    }
}