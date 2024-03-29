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
    [Table("Najemca")]
    public class TenantModel
    {
        [Key]
        public int TenantID { get; set; }
        [MaxLength(20)]
        [DisplayName("Imię")]
        public string Name { get; set; }
        [MaxLength(30)]
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }


        [Range(18, 90)]
        [DisplayName("Wiek")]
        public int Age { get; set; }

        [DisplayName("Palisz?")]
        public bool IsSmoking { get; set; }
        [DisplayName("Jesteś wege?")]
        public bool IsVege { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        public string Gender { get; set; }
        [ForeignKey("UserID")]
        [DisplayName("Płeć")]
        public int UserID { get; set; }
      
        public virtual UserModel User { get; set; }
    }
}
