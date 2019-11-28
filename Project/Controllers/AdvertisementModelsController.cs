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
    public class AdvertisementModelsController : Controller
    {
        private readonly ProjectContext _context;

        public AdvertisementModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: AdvertisementModels
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Advertisements.Include(a => a.Flat).Include(a => a.Owner);
            return View(await projectContext.ToListAsync());
        }

        // GET: AdvertisementModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementModel = await _context.Advertisements
                .Include(a => a.Flat)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AdvertisementID == id);
            if (advertisementModel == null)
            {
                return NotFound();
            }

            return View(advertisementModel);
        }

        // GET: AdvertisementModels/Create
        public IActionResult Create()
        {
            ViewData["FlatID"] = new SelectList(_context.Flats, "FlatID", "FlatID");
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "OwnerID");
            return View();
        }

        // POST: AdvertisementModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvertisementID,AdvertisementType,OwnerID,FlatID")] AdvertisementModel advertisementModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisementModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlatID"] = new SelectList(_context.Flats, "FlatID", "FlatID", advertisementModel.FlatID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "OwnerID", advertisementModel.OwnerID);
            return View(advertisementModel);
        }

        // GET: AdvertisementModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementModel = await _context.Advertisements.FindAsync(id);
            if (advertisementModel == null)
            {
                return NotFound();
            }
            ViewData["FlatID"] = new SelectList(_context.Flats, "FlatID", "FlatID", advertisementModel.FlatID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "OwnerID", advertisementModel.OwnerID);
            return View(advertisementModel);
        }

        // POST: AdvertisementModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertisementID,AdvertisementType,OwnerID,FlatID")] AdvertisementModel advertisementModel)
        {
            if (id != advertisementModel.AdvertisementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisementModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementModelExists(advertisementModel.AdvertisementID))
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
            ViewData["FlatID"] = new SelectList(_context.Flats, "FlatID", "FlatID", advertisementModel.FlatID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "OwnerID", advertisementModel.OwnerID);
            return View(advertisementModel);
        }

        // GET: AdvertisementModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementModel = await _context.Advertisements
                .Include(a => a.Flat)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AdvertisementID == id);
            if (advertisementModel == null)
            {
                return NotFound();
            }

            return View(advertisementModel);
        }

        // POST: AdvertisementModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisementModel = await _context.Advertisements.FindAsync(id);
            _context.Advertisements.Remove(advertisementModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementModelExists(int id)
        {
            return _context.Advertisements.Any(e => e.AdvertisementID == id);
        }
    }
}
