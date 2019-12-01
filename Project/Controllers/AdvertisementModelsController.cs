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
        private readonly List<string> adType = new List<string>(new string[] { "całe mieszkanie", "pojedynczy pokój" });

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

        public async Task<IActionResult> MyAds()
        {
            var id = this.HttpContext.Session.GetString("UserID");
            if (id != null)
            {

                var user = await _context.User
                    .FirstOrDefaultAsync(m => m.UserID == int.Parse(this.HttpContext.Session.GetString("UserID")));
                var owner = await _context.Owners
                   .FirstOrDefaultAsync(m => m.UserID == int.Parse(this.HttpContext.Session.GetString("UserID")));   
                if (user.Login == "admin")
                {
                    var ad = _context.Advertisements;
                    return View(ad.ToList());
                }
                if (owner == null) return RedirectToAction("Create", "OwnerModels");
                var ads = _context.Advertisements.Where(m => m.Owner.Equals(owner)).ToList();
                return View(ads);
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
                .Include(a => a.Flat.City)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AdvertisementID == id);
            if (advertisementModel == null)
            {
                return NotFound();
            }
            var roomModel = await _context.Rooms
                .Include(a => a.Flat)
                .Include(a => a.Flat.City)
                .FirstOrDefaultAsync(m => m.FlatID == advertisementModel.FlatID);
            ViewBag.RoomSurface = roomModel.Surface;
            ViewBag.RoomWardrobe = roomModel.Wardrobe ? "TAK" : "NIE";
            ViewBag.RoomBalcony = roomModel.Balcony ? "TAK" : "NIE";
            ViewBag.RoomBed = roomModel.Bed ? "TAK" : "NIE";
            ViewBag.RoomAdd = roomModel.AdditionalInfo;
            ViewBag.RoomRent = roomModel.Rent;
            return View(advertisementModel);
        }


        // GET: AdvertisementModels/Create
        public IActionResult Create()
        {
            var id = this.HttpContext.Session.GetString("UserID");
            if (id != null)
            {
                ViewData["CityName"] = new SelectList(_context.Citys, "CityName", "CityName");
                ViewData["AdType"] = new SelectList(adType);
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
            var cityModel = _context.Citys.ToList();
            flatModel = FlatModel.setCity(city, flatModel, cityModel);
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

            var advertisementModel = await _context.Advertisements
                .Include(a => a.Flat)
                .Include(a => a.Flat.City)
                .FirstOrDefaultAsync(m => m.AdvertisementID.Equals(id));
            if (advertisementModel == null)
            {
                return NotFound();
            }

            var roomModel = await _context.Rooms
                .Include(a => a.Flat)
                .FirstOrDefaultAsync(m => m.FlatID.Equals(advertisementModel.FlatID));

            ViewData["Adv"] = advertisementModel;
            ViewData["Flat"] = advertisementModel.Flat;
            ViewData["AdType"] = new SelectList(adType);
            ViewData["Room"] = roomModel;
            ViewData["CityName"] = new SelectList(_context.Citys, "CityName", "CityName");
            return View();
        }

        // POST: AdvertisementModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(Prefix ="Item1")] AdvertisementModel advertisementModel,
            [Bind(Prefix = "Item2")] FlatModel flatModel, [Bind(Prefix = "Item3")] RoomModel roomModel, string city)
        {
            var cityModel = _context.Citys.ToList();
            var ownerModel = _context.Owners.ToList();
            flatModel = FlatModel.setCity(city, flatModel, cityModel);
            advertisementModel.Flat = flatModel;
            advertisementModel = AdvertisementModel.setOwner(advertisementModel.OwnerID, advertisementModel, ownerModel);
            roomModel.Flat = flatModel;
            if (id != advertisementModel.AdvertisementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flatModel);
                    _context.Update(roomModel);
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
                .Include(a => a.Flat.City)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AdvertisementID == id);
            if (advertisementModel == null)
            {
                return NotFound();
            }

            return RedirectToAction("Delete", "FlatModels", new { id = advertisementModel.FlatID });
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
