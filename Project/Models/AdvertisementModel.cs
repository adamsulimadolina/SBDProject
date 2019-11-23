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
    [Table("Ogłoszenie")]
    public class AdvertisementModel
    {
        [Key]
        public int AdvertisementID { get; set; }
        [StringLength(40)]
        public string AdvertisementType { get; set; }

        [ForeignKey("OwnerID")]
        public int OwnerID { get; set; }
        [ForeignKey("FlatID")]
        public int FlatID { get; set; }

        public virtual OwnerModel Owner { get; set; }
        public virtual FlatModel Flat { get; set; }


    }
}
