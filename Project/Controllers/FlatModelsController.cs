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
    public class FlatModelsController : Controller
    {
        private readonly ProjectContext _context;

        public FlatModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: FlatModels
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Flats.Include(f => f.City);
            return View(await projectContext.ToListAsync());
        }

        // GET: FlatModels/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: FlatModels/Create
        public IActionResult Create()
        {
            ViewData["CityID"] = new SelectList(_context.Citys, "CityName", "CityName");
            ViewData["CityName"] = new SelectList(_context.Citys, "CityName", "CityName");
            ViewData["CityID"] = new SelectList(_context.Citys, "CityName", "CityName", "CityID", "CityID");
            return View();
        }

        // POST: FlatModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlatID,RoomsCount,BathroomCount,Surface,KitchenType")] FlatModel flatModel, string city)
        {
            var cityModel = await _context.Citys.ToListAsync();
            foreach(var elem in cityModel)
            {
                if(elem.CityName == city)
                {
                    flatModel.CityID = elem.CityID;
                    break;
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(flatModel);
                await _context.SaveChangesAsync();
                return RedirectToAction();
            }
            ViewData["CityID"] = new SelectList(_context.Citys, "CityID", "CityID", flatModel.City);
            return View(flatModel);
        }

        // GET: FlatModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flatModel = await _context.Flats.FindAsync(id);
            if (flatModel == null)
            {
                return NotFound();
            }
            ViewData["CityID"] = new SelectList(_context.Citys, "CityID", "CityID", flatModel.CityID);
            return View(flatModel);
        }

        // POST: FlatModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlatID,RoomsCount,BathroomCount,Surface,KitchenType,CityID")] FlatModel flatModel)
        {
            if (id != flatModel.FlatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flatModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlatModelExists(flatModel.FlatID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityID"] = new SelectList(_context.Citys, "CityID", "CityID", flatModel.CityID);
            return View(flatModel);
        }

        // GET: FlatModels/Delete/5
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
            return RedirectToAction(nameof(Index));
        }

        private bool FlatModelExists(int id)
        {
            return _context.Flats.Any(e => e.FlatID == id);
        }
    }
}
