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
    public class OpinionModel
    {
        [Required]
        public int OpinionID { get; set; }

        public string Opinion { get; set; }

        public int OwnerID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("OwnerID")]
        public virtual OwnerModel Owner { get; set; }

        [ForeignKey("UserID")]
        public virtual UserModel User { get; set; }
    }
}
