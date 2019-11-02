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
    [Table("User")]
    public class UserModel
    {       
        [Key]
        public int UserID { get; set; }
        [MaxLength(20),MinLength(4)]
        public string Login { get; set; }
        [MinLength(5),MaxLength(25)]
        public string Password { get; set; }
        public bool Verified { get; set; }
    }
}
