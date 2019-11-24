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
    public class PairsModelsController : Controller
    {
        private readonly ProjectContext _context;
        private bool x = false;

        public PairsModelsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: PairsModels
        public async Task<IActionResult> Index()
        {
            var id = this.HttpContext.Session.GetString("UserID");//id zalogowanego użytkownika
            
            

                var tenantLog = _context.Tenants.Where(m => m.UserID == int.Parse(id)).Select(m => m.TenantID).ToList();//tenantid zalegowanego użytkownika
                var pairsList = _context.Pairs.Where(m => m.TenantID_1 == tenantLog[0]).ToList();//lista dopasowań dla zalogowanego użytkownika
               
           
               
            
            if (pairsList.Count == 0)
            {
                var tenantList = _context.Tenants.Where(m => m.UserID != int.Parse(id)).ToList();
                var tp = _context.Tenants.Where(m => m.UserID == int.Parse(id)).Select(m => new { m.Age, m.Status, m.Gender, m.IsSmoking, m.IsVege }).ToList();
                var a = tp.Count;
                foreach (var item in tenantList)
                {
                    PairsModel pairsModel = new PairsModel();
                  
                    float compatibility = 200;


                    var gep = tp[0].Gender;
                    var ged = item.Gender;
                    if (gep.Equals(ged))
                    {
                        compatibility += 30;
                    }
                    else
                    {
                        compatibility -= 30;
                    }

                    if (tp[0].IsSmoking == item.IsSmoking)
                    {
                        compatibility += 50;
                    }
                    else
                    {
                        compatibility -= 50;
                    }

                    if (tp[0].IsVege == item.IsVege)
                    {
                        compatibility += 50;
                    }
                    else
                    {
                        compatibility -= 50;
                    }
                    var stp = tp[0].Status;
                    var std = item.Status;
                    compatibility += calculateStatus(stp, std);


                    var agp = tp[0].Age;
                    var agd = item.Age;
                    compatibility += calculateAge(agp, agd);
                    compatibility /= 4;
                    pairsModel.TenantID_1 = tenantLog[0];
                    pairsModel.TenantID_2 = item.TenantID;
                    pairsModel.PairCompatibility = compatibility;

                    _context.Add(pairsModel);

                 //   pairsModel.TenantID_2 = int.Parse(id);
                   // pairsModel.TenantID_1 = item.TenantID;
                    //pairsModel.PairCompatibility = compatibility ;


                    //_context.Add(pairsModel);


                }

            }

            await _context.SaveChangesAsync();
            var pairsList2 = _context.Pairs.Where(m => m.TenantID_1 == tenantLog[0]).ToList();//lista dopasowań dla zalogowanego użytkownika
            List<PairsModel> pairs = new List<PairsModel>();
            foreach (var item in pairsList2)
            {
                pairs.Add(item);
            }
            return View(pairs);
        }


        public async Task<IActionResult> CreatePairs(PairsModel pairsModel)
        {
           

            return RedirectToAction("Create", "PairsModels", pairsModel);



        }


        // GET: PairsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pairsModel = await _context.Pairs
                .FirstOrDefaultAsync(m => m.PairID == id);
            if (pairsModel == null)
            {
                return NotFound();
            }

            return View(pairsModel);
        }

        public int calculateStatus(string stp, string std)
        {
            if (stp.Equals(std))
            {
                return 35;
            }
            else if (stp.Equals("Unemployed") || std.Equals("Unemployed"))
            {
                return -20;

            }
            else if ((stp.Equals("Pensioner") || std.Equals("Pensioner")))
            {
                return -35;
            }
            else if ((stp.Equals("Employee") || std.Equals("Employee")))
            {
                return -25;
            }
           
                return -30;
            
        }
        
        public int calculateAge(int agp, int agd)
        {
            if (agp >= 18 && agp < 27)
            {
                if (agd >= 18 && agd < 25)
                {
                    return 35;
                }
                else if (agd >= 25 && agd < 37)
                {
                    return -10;
                }
                else if (agd >= 37 && agd < 47)
                {
                    return -15;
                }
                else if (agd >= 47 && agd < 55)
                {
                    return -20;
                }
                else if (agd >= 55)
                {
                    return -25;
                }
            }
            else if (agp >= 25 && agp < 37)
            {
                if (agd >= 18 && agd < 25)
                {
                    return -10;
                }
                else if (agd >= 25 && agd < 37)
                {
                    return 35;
                }
                else if (agd >= 37 && agd < 47)
                {
                    return -10;
                }
                else if (agd >= 47 && agd < 55)
                {
                    return -15;
                }
                else if (agd >= 55)
                {
                    return -20;
                }
            }
            else if (agp >= 37 && agp < 47)
            {
                if (agd >= 18 && agd < 25)
                {
                    return -15;
                }
                else if (agd >= 25 && agd < 37)
                {
                    return -10;
                }
                else if (agd >= 37 && agd < 47)
                {
                    return 35;
                }
                else if (agd >= 47 && agd < 55)
                {
                    return -10;
                }
                else if (agd >= 55)
                {
                    return -15;
                }
            }
            else if (agp >= 47 && agp < 55)
            {
                if (agd >= 18 && agd < 25)
                {
                    return -20;
                }
                else if (agd >= 25 && agd < 37)
                {
                    return - 15;
                }
                else if (agd >= 37 && agd < 47)
                {
                    return - 10;
                }
                else if (agd >= 47 && agd < 55)
                {
                    return 35;
                }
                else if (agd >= 55)
                {
                    return - 15;
                }
            }
            else if (agp >= 55)
            {
                if (agd >= 18 && agd < 25)
                {
                    return - 25;
                }
                else if (agd >= 25 && agd < 37)
                {
                    return - 20;
                }
                else if (agd >= 37 && agd < 47)
                {
                    return - 15;
                }
                else if (agd >= 47 && agd < 55)
                {
                    return - 10;
                }
                else if (agd >= 55)
                {
                    return 35;
                }
            }
            return 0;
        }
            

        // GET: PairsModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PairsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void Create([Bind("PairID,PairCompatibility,TenantID_1,TenantID_2")] PairsModel pairsModel)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(pairsModel);
                await _context.SaveChangesAsync();
                
               // return RedirectToAction(nameof(Index));
            }
           // return View(pairsModel);
        }

       

       

        // GET: PairsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pairsModel = await _context.Pairs.FindAsync(id);
            if (pairsModel == null)
            {
                return NotFound();
            }
            return View(pairsModel);
        }

        // POST: PairsModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PairID,PairCompatibility,TenantID_1,TenantID_2")] PairsModel pairsModel)
        {
            if (id != pairsModel.PairID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pairsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PairsModelExists(pairsModel.PairID))
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
            return View(pairsModel);
        }

        // GET: PairsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pairsModel = await _context.Pairs
                .FirstOrDefaultAsync(m => m.PairID == id);
            if (pairsModel == null)
            {
                return NotFound();
            }

            return View(pairsModel);
        }

        // POST: PairsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pairsModel = await _context.Pairs.FindAsync(id);
            _context.Pairs.Remove(pairsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PairsModelExists(int id)
        {
            return _context.Pairs.Any(e => e.PairID == id);
        }
    }
}
