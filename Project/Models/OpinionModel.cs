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
    [Table("Opinie")]
    public class OpinionModel
    {
        [Key]
        public int OpinionID { get; set; }
        [MaxLength(250)]
        public string Opinion { get; set; }

        [ForeignKey("OwnerID")]
        public int OwnerID { get; set; }

        [ForeignKey("UserID")]
        
        public int? UserID { get; set; }
       
        public virtual OwnerModel Owner { get; set; }
        
        public virtual UserModel User { get; set; }
    }
}
