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
    [Table("Najemca")]
    public class TenantModel
    {
        [Key]
        public int TenantID { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Surname { get; set; }
        
        public int Age { get; set; }
        [Required]
        public bool IsSmoking { get; set; }
        
        public bool IsVege { get; set; }

        public string Status { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
      
        public virtual UserModel User { get; set; }
    }
}
