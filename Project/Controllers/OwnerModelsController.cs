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
    public class OwnerModelsController : Controller
    {
        private readonly ProjectContext _context;

        public OwnerModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: OwnerModels
        public async Task<IActionResult> Index()
        {
            var id = this.HttpContext.Session.GetString("UserID");

            if (id != null)
            {
                ViewBag.AbletoModify = int.Parse(id);
                var tmp = _context.Owners.Where(m => m.UserID == int.Parse(id)).Select(m => m.UserID).ToList();

                if (tmp.Count != 0)
                {
                    return View(await _context.Owners.ToListAsync());
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: OwnerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerModel = await _context.Owners
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OwnerID == id);
            if (ownerModel == null)
            {
                return NotFound();
            }

            return View(ownerModel);
        }

        // GET: OwnerModels/Create
        public IActionResult Create()
        {
            var id = this.HttpContext.Session.GetString("UserID");

            if (id != null)
            {
                ViewBag.AbletoModify = int.Parse(id);
                var tmp = _context.Owners.Where(m => m.UserID == int.Parse(id)).Select(m => m.UserID).ToList();

                if (tmp.Count != 0)
                {
                    return RedirectToAction("Index", "AdvertisementModels");
                }
            }
                if (TempData["ModelState"] != null)
            {
                ModelState.AddModelError(string.Empty, (string)TempData["ModelState"]);
            }
            //  ViewData["UserID"] = new SelectList(_context.Set<UserModel>(), "UserID", "UserID");

            return View();
        }

        // POST: OwnerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerID,Name,Surname,UserID")] OwnerModel ownerModel)
        {
            if (ModelState.IsValid)
            {

                var id = this.HttpContext.Session.GetString("UserID");
                ownerModel.UserID = int.Parse(id);
                _context.Add(ownerModel);
                await _context.SaveChangesAsync();
                ModelState.Clear();

                return RedirectToAction("Index", "AdvertisementModels");
            }
            TempData["ModelState"] = "You must fill in all of the fields";
            //ViewData["UserID"] = new SelectList(_context.Set<UserModel>(), "UserID", "UserID", tenantModel.UserID);
            return RedirectToAction("Create");
        }

        // GET: OwnerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerModel = await _context.Owners.FindAsync(id);
            if (ownerModel == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Login", ownerModel.UserID);
            return View(ownerModel);
        }

        // POST: OwnerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OwnerID,Name,Surname,UserID")] OwnerModel ownerModel)
        {
            if (id != ownerModel.OwnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerModelExists(ownerModel.OwnerID))
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
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "Login", ownerModel.UserID);
            return View(ownerModel);
        }

        // GET: OwnerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerModel = await _context.Owners
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OwnerID == id);
            if (ownerModel == null)
            {
                return NotFound();
            }

            return View(ownerModel);
        }

        // POST: OwnerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownerModel = await _context.Owners.FindAsync(id);
            _context.Owners.Remove(ownerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerModelExists(int id)
        {
            return _context.Owners.Any(e => e.OwnerID == id);
        }
    }
}
