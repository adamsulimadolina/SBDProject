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

namespace Project.Models
{
    [Table("Parowanie")]
    public class PairsModel
    {
        [Key]
        public int PairID { get; set; }
        [Required]
        public double PairCompatibility { get; set; }

        [ForeignKey("TenantID_1")]
        public int TenantID_1 { get; set; }
        [ForeignKey("TenantID_2")]
        public int TenantID_2 { get; set; }

        public virtual TenantModel Tenant_1 { get; set; }

        public virtual TenantModel Tenant_2 { get; set; }


    }
}
