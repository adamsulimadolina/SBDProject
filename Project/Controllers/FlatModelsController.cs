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
    public class FlatModelsController : Controller
    {
        private readonly ProjectContext _context;

        public FlatModelsController(ProjectContext context)
        {
            _context = context;
        }

        //// GET: FlatModels
        //public async Task<IActionResult> Index()
        //{
        //    if(!Methods.checkAdmin(this.HttpContext.Session.GetString("UserID"), _context))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    var projectContext = _context.Flats.Include(f => f.City);
        //    return View(await projectContext.ToListAsync());
        //}

        //// GET: FlatModels/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var flatModel = await _context.Flats
        //        .Include(f => f.City)
        //        .FirstOrDefaultAsync(m => m.FlatID == id);
        //    if (flatModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(flatModel);
        //}
        //// GET: FlatModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flatModel = await _context.Flats
                .Include(f => f.City)
                .FirstOrDefaultAsync(m => m.FlatID == id);
            if (flatModel == null)
            {
                return NotFound();
            }

            return View(flatModel);
        }

        // POST: FlatModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flatModel = await _context.Flats.FindAsync(id);
            _context.Flats.Remove(flatModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdvertisementModels");
        }

        private bool FlatModelExists(int id)
        {
            return _context.Flats.Any(e => e.FlatID == id);
        }
    }
}
