﻿using System;
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
using System.ComponentModel;

namespace Project.Models
{
    [Table("Wlasciciel")]
    public class OwnerModel
    {
        [Key]
        public int OwnerID { get; set; }
        [MaxLength(20)]
        [DisplayName("Imię właściciela")]
        public string Name { get; set; }
        [MaxLength(30)]
        [DisplayName("Nazwisko właściciela")]
        public string Surname { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }

       
        public virtual UserModel User {get; set;}
    }
}
