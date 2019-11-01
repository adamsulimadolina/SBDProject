using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBDProject.Models
{
    public class TenantModel
    {
        [Required]
        public int TenantID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public bool IsSmoking { get; set; }

        public bool IsVege { get; set; }

        public string Status { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserModel User { get; set; }
    }
}
