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
    public class OpinionModelsController : Controller
    {
        private readonly ProjectContext _context;

        public OpinionModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: OpinionModels
        public async Task<IActionResult> Index(int? id)
        {

            ViewBag.AbletoModify = int.Parse(this.HttpContext.Session.GetString("UserID"));
            var opinionList = _context.Opinions.Where(m => m.OwnerID == id).ToList();
            return View(opinionList);
        }

      
      

        //zmi
        // GET: OpinionModels/Create
        public IActionResult Create()
        {
          
           
            return View();
        }

        // POST: OpinionModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpinionID,Opinion,OwnerID,UserID")] OpinionModel opinionModel,int id)
        {
            if (ModelState.IsValid)
            {
               
                opinionModel.OwnerID = id;

                var idLog = this.HttpContext.Session.GetString("UserID");
                opinionModel.UserID = int.Parse(idLog);
                _context.Add(opinionModel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "AdvertisementModels");
            }
           // ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "OwnerID", opinionModel.OwnerID);
           
            return View(opinionModel);
        }

        // GET: OpinionModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinionModel = await _context.Opinions.FindAsync(id);
            if (opinionModel == null)
            {
                return NotFound();
            }
          
            return View(opinionModel);
        }

        // POST: OpinionModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpinionID,Opinion,OwnerID,UserID")] OpinionModel opinionModel)
        {
            if (id != opinionModel.OpinionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opinionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpinionModelExists(opinionModel.OpinionID))
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
            ViewData["OwnerID"] = new SelectList(_context.Owners, "OwnerID", "OwnerID", opinionModel.OwnerID);
            return View(opinionModel);
        }

        // GET: OpinionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinionModel = await _context.Opinions
                .Include(o => o.Owner)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OpinionID == id);
            if (opinionModel == null)
            {
                return NotFound();
            }

            return View(opinionModel);
        }

        // POST: OpinionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opinionModel = await _context.Opinions.FindAsync(id);
            _context.Opinions.Remove(opinionModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpinionModelExists(int id)
        {
            return _context.Opinions.Any(e => e.OpinionID == id);
        }
    }
}
