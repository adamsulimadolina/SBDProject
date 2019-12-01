using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View(await _context.Logs
                    .Include(m => m.UserR)
                    .ToListAsync());
        }
    }
}
