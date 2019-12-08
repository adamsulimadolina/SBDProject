using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Controllers;
using Project.Data;
using Project.Models;

namespace SBDProject.Controllers
{
    public class TenantModelsController : Controller
    {
        private readonly ProjectContext _context;
        private readonly List<string> typeofStatus = new List<string>(new string[] { "Student", "Pracujący", " Emeryta", "Niepracujący" });
        private readonly List<string> typeofGender = new List<string>(new string[] { "Kobieta", "Mężczyzna" });


        public TenantModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: TenantModels
        public async Task<IActionResult> Index()
        {
           
            // var applicationDbContext = _context.TenantModel.Include(t => t.User);
            var id = this.HttpContext.Session.GetString("UserID");
           
            if (id != null)
            {
                ViewBag.AbletoModify = int.Parse(id);
                var tmp = _context.Tenants.Where(m => m.UserID == int.Parse(id)).Select(m => m.UserID).ToList();

                if (tmp.Count != 0)
                {
                    return View(await _context.Tenants.ToListAsync());
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

        // GET: TenantModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantModel = await _context.Tenants
                .FirstOrDefaultAsync(m => m.TenantID == id);
            if (tenantModel == null)
            {
                return NotFound();
            }

            return View(tenantModel);
        }

        // GET: TenantModels/Create
        public IActionResult Create()
        {
            var query1 = from tenant in _context.Tenants                         
                         select tenant;
            var tenants = query1.ToList();
            foreach(var tenant in tenants)
            {
                if(tenant.UserID== int.Parse(this.HttpContext.Session.GetString("UserID")))
                {                                      
                    return RedirectToAction("Index","PairsModels");
                }
            }
            if (TempData["ModelState"] != null)
            {
                ModelState.AddModelError(string.Empty, (string)TempData["ModelState"]);
            }
            //  ViewData["UserID"] = new SelectList(_context.Set<UserModel>(), "UserID", "UserID");
            ViewData["TypeofStatus"] = new SelectList(typeofStatus);
            ViewData["TypeofGender"] = new SelectList(typeofGender);
            return View();
        }

        // POST: TenantModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,Name,Surname,Age,IsSmoking,IsVege,Status,Gender,UserID")] TenantModel tenantModel)
        {
            
            if (ModelState.IsValid)
            {

                var id = this.HttpContext.Session.GetString("UserID");
                var user = _context.User.Where(m => m.UserID == int.Parse(id)).ToList();
                tenantModel.UserID = int.Parse(id);
                tenantModel.User = user[0];
                _context.Add(tenantModel);
                await _context.SaveChangesAsync();
                ModelState.Clear();
                return RedirectToAction("Index", "PairsModels");
            }
            TempData["ModelState"] = "You must fill in all of the fields";
            //ViewData["UserID"] = new SelectList(_context.Set<UserModel>(), "UserID", "UserID", tenantModel.UserID);
            return RedirectToAction("Create");
        }

        // GET: TenantModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantModel = await _context.Tenants.FindAsync(id);
            if (tenantModel == null)
            {
                return NotFound();
            }
            // ViewData["UserID"] = new SelectList(_context.Set<UserModel>(), "UserID", "UserID", tenantModel.UserID);
            ViewData["TypeofStatus"] = new SelectList(typeofStatus);
            ViewData["TypeofGender"] = new SelectList(typeofGender);
            return View(tenantModel);
        }

        // POST: TenantModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantID,Name,Surname,Age,IsSmoking,IsVege,Status,Gender,UserID")] TenantModel tenantModel)
        {
            if (id != tenantModel.TenantID)
            {
                return NotFound();
            }

            tenantModel.User = _context.User.Find(tenantModel.UserID);
            if (ModelState.IsValid)
            {
                try
                {
                    //tenantModel.UserID = (string)TempData["UserID"];
                    _context.Update(tenantModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantModelExists(tenantModel.TenantID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Account");
            }
            //   ViewData["UserID"] = new SelectList(_context.Set<UserModel>(), "UserID", "UserID", tenantModel.UserID);
            return View(tenantModel);
        }

        // GET: TenantModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantModel = await _context.Tenants
                .FirstOrDefaultAsync(m => m.TenantID == id);
            if (tenantModel == null)
            {
                return NotFound();
            }

            return View(tenantModel);
        }

        // POST: TenantModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TenantModel tenantModel = _context.Tenants.Where(m => m.TenantID == id).SingleOrDefault();
            var pairs = _context.Pairs.Where(m => m.TenantID_1 == id).ToList();
            var pairs2 = _context.Pairs.Where(m => m.TenantID_2 == id).ToList();
            foreach(var pr in pairs)
            {
                
                _context.Pairs.Remove(pr);
            }
            foreach(var pr in pairs2)
            {
                _context.Pairs.Remove(pr);
            }
            _context.Tenants.Remove(tenantModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Show()
        {
            return RedirectToAction("Index", "PairsModels");
        }

        private bool TenantModelExists(int id)
        {

            return _context.Tenants.Any(e => e.TenantID == id);
        }
    }
}
