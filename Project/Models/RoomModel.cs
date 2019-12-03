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
using System.ComponentModel;

namespace Project.Models
{
    [Table("Pokoj")]
    public class RoomModel
    {
        [Key]
        public int RoomID { get; set; }
        [Required]
        [DisplayName("Powierzchnia całkowita")]
        public double Surface {get;set;}
        [DisplayName("Balkon")]
        public bool Balcony { get; set; }
        [DisplayName("Łóżko")]
        public bool Bed { get; set; }
        [DisplayName("Szafa")]
        public bool Wardrobe { get; set; }
        [MaxLength(40)]
        [DisplayName("Dodatkowe informacje")]
        public string AdditionalInfo { get; set; }
        [Required]
        [DisplayName("Cena")]
        public double Rent { get; set; }

        [ForeignKey("FlatID")]
        public int FlatID { get; set; }

        public virtual FlatModel Flat { get; set; }

        

    }
}
