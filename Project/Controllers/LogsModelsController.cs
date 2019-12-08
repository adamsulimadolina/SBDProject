using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class LogsModelsController : Controller
    {
        private readonly ProjectContext _context;

        public LogsModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: LogsModels
        public async Task<IActionResult> Index()
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            return View(await _context.Logs
                    .Include(m => m.UserR)
                    .OrderByDescending(m => m.MessageDate)
                    .ToListAsync());
        }
    }
}
