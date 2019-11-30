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
            var id = this.HttpContext.Session.GetString("UserID");

            if (id != null)
            {
                return View(await _context.Advertisements.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

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
            var id = this.HttpContext.Session.GetString("UserID");
            if (id != null)
            {
                ViewData["CityName"] = new SelectList(_context.Citys, "CityName", "CityName");
                ViewBag.AbletoModify = int.Parse(id);
                var tmp = _context.Owners.Where(m => m.UserID == int.Parse(id)).Select(m => m.UserID).ToList();

                if (tmp.Count != 0)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Create", "OwnerModels");
                }
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] AdvertisementModel advertisementModel,
            [Bind(Prefix = "Item2")] FlatModel flatModel, string city, [Bind(Prefix = "Item3")] RoomModel roomModel)
        {
            //var cityModel = await _context.Citys.ToListAsync();
            //foreach (var elem in cityModel)
            //{
            //    if (elem.CityName == city)
            //    {
            //        flatModel.CityID = elem.CityID;
            //        break;
            //    }
            //}
            flatModel.setCity(city, _context);
            if (ModelState.IsValid)
            {
                _context.Add(flatModel);
                await _context.SaveChangesAsync();

                var flats = await _context.Flats.ToListAsync();
                var tmp_id = 1;
                foreach (var flat in flats)
                {
                    if (flat.FlatID > tmp_id) tmp_id = flat.FlatID;
                }

                var this_flat = await _context.Flats
                    .FirstOrDefaultAsync(m => m.FlatID == tmp_id);
                    
                var this_owner = await _context.Owners
                    .FirstOrDefaultAsync(m => m.UserID == int.Parse(this.HttpContext.Session.GetString("UserID")));


                advertisementModel.Flat = this_flat;
                advertisementModel.FlatID = this_flat.FlatID;
                advertisementModel.Owner = this_owner;
                advertisementModel.OwnerID = this_owner.OwnerID;
                roomModel.Flat = this_flat;
                roomModel.FlatID = this_flat.FlatID;

                _context.Add(roomModel);
                _context.Add(advertisementModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
