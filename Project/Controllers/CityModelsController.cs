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
    public class CityModelsController : Controller
    {
        private readonly ProjectContext _context;

        public CityModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: CityModels
        public async Task<IActionResult> Index()
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");
 
            return View(await _context.Citys.ToListAsync());
        }

        // GET: CityModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var cityModel = await _context.Citys
                .FirstOrDefaultAsync(m => m.CityID == id);
            if (cityModel == null)
            {
                return NotFound();
            }

            return View(cityModel);
        }

        // GET: CityModels/Create
        public IActionResult Create()
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: CityModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityID,CityName")] CityModel cityModel)
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.Add(cityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cityModel);
        }

        // GET: CityModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var cityModel = await _context.Citys.FindAsync(id);
            if (cityModel == null)
            {
                return NotFound();
            }
            return View(cityModel);
        }

        // POST: CityModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityID,CityName")] CityModel cityModel)
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            if (id != cityModel.CityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityModelExists(cityModel.CityID))
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
            return View(cityModel);
        }

        // GET: CityModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var cityModel = await _context.Citys
                .FirstOrDefaultAsync(m => m.CityID == id);
            if (cityModel == null)
            {
                return NotFound();
            }

            return View(cityModel);
        }

        // POST: CityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usr_id = this.HttpContext.Session.GetString("UserID");
            if (usr_id == null) return RedirectToAction("Login", "Account");
            if (!Methods.checkAdmin(int.Parse(usr_id), _context)) return RedirectToAction("Index", "Home");

            var cityModel = await _context.Citys.FindAsync(id);
            _context.Citys.Remove(cityModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityModelExists(int id)
        {
            return _context.Citys.Any(e => e.CityID == id);
        }
    }
}
