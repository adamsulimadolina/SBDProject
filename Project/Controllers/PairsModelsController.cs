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
            var users = _context.User.Where(m => m.UserID != int.Parse(id)).ToList();
            var count = pairsList.Count;
            if (pairsList.Count < users.Count)
            {
                Pair(tenantLog[0], 0);
                Pair(0, tenantLog[0]);
            }

            await _context.SaveChangesAsync();
            var pairsList2 = _context.Pairs.Where(m => m.TenantID_1 == tenantLog[0]).ToList();//lista dopasowań dla zalogowanego użytkownika
            List<PairsModel> pairs = new List<PairsModel>();
            foreach (var item in pairsList2)
            {

                pairs.Add(item);
            }
            for (int i = 0; i < pairsList2.Count(); i++)
            {
                for (int j = i + 1; j < pairsList2.Count(); j++)
                {
                    if (pairsList2[i].TenantID_1 == pairsList2[j].TenantID_1 &&
                  pairsList2[i].TenantID_2 == pairsList2[j].TenantID_2)
                    {
                        pairs.Remove(pairsList2[i]);
                        _context.Pairs.Remove(pairsList2[i]);
                    }
                }

            }
            return View(pairs);
        }

        public void Pair(int Tenant1, int Tenant2)
        {
            var id = this.HttpContext.Session.GetString("UserID");//id zalogowanego użytkownika
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
                if (Tenant1 == 0)
                {
                    pairsModel.TenantID_2 = Tenant2;
                    pairsModel.TenantID_1 = item.TenantID;
                }
                else
                {
                    pairsModel.TenantID_1 = Tenant1;
                    pairsModel.TenantID_2 = item.TenantID;
                }

                pairsModel.PairCompatibility = compatibility;

                _context.Add(pairsModel);
            }
        }


        public async Task<IActionResult> CreatePairs(PairsModel pairsModel)
        {
            return RedirectToAction("Create", "PairsModels", pairsModel);
        }
        public async Task<IActionResult> Back()
        {
            return RedirectToAction("Index", "TenantModels");
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
            if (agp >= 18 && agp < 25)
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
                    return -15;
                }
                else if (agd >= 37 && agd < 47)
                {
                    return -10;
                }
                else if (agd >= 47 && agd < 55)
                {
                    return 35;
                }
                else if (agd >= 55)
                {
                    return -15;
                }
            }
            else if (agp >= 55)
            {
                if (agd >= 18 && agd < 25)
                {
                    return -25;
                }
                else if (agd >= 25 && agd < 37)
                {
                    return -20;
                }
                else if (agd >= 37 && agd < 47)
                {
                    return -15;
                }
                else if (agd >= 47 && agd < 55)
                {
                    return -10;
                }
                else if (agd >= 55)
                {
                    return 35;
                }
            }
            return 0;
        }

        private bool PairsModelExists(int id)
        {
            return _context.Pairs.Any(e => e.PairID == id);
        }
    }
}
