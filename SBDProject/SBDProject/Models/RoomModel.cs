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
    [Table("Pokoj")]
    public class RoomModel
    {
        [Key]
        public int RoomID { get; set; }
        [Required]
        public double Surface {get;set;}

        public bool Balcony { get; set; }

        public bool Bed { get; set; }

        public bool Wardrobe { get; set; }
        [MaxLength(40)]
        public string AdditionalInfo { get; set; }
        [Required]
        public double Rent { get; set; }

        [ForeignKey("FlatID")]
        public int FlatID { get; set; }

        public virtual FlatModel Flat { get; set; }

    }
}
