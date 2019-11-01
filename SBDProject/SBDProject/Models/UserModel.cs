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
    public class UserModel
    {
        [Required]
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; }
    }
}
