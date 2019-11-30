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
    [Table("Mieszkanie")]
    public class FlatModel
    {
        
        [Key]
        public int FlatID { get; set; }
        
        public int RoomsCount { get; set; }
        public int BathroomCount { get; set; }

        [Required]
        public double Surface { get; set; }
        [StringLength(20)]
        public string KitchenType { get; set; }
        
        [ForeignKey("CityID")]
        public int CityID { get; set; }

        public virtual CityModel City { get; set; }

    }
}
