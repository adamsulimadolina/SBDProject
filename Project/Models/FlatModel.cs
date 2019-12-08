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
using Project.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace Project.Models
{
    [Table("Mieszkanie")]
    public class FlatModel
    {
        
        [Key]
        public int FlatID { get; set; }

        [DisplayName("Liczba pokoi")]
        public int RoomsCount { get; set; }
        [DisplayName("Liczba łazienek")]
        public int BathroomCount { get; set; }

        [Required]
        [DisplayName("Powierzchnia całkowita")]
        public int Surface { get; set; }
        [StringLength(20)]
        [DisplayName("Typ kuchni")]
        public string KitchenType { get; set; }

        [ForeignKey("CityID")]
        public int CityID { get; set; }

        [DisplayName("Miasto")]
        public virtual CityModel City { get; set; }

        public static FlatModel setCity(string city, FlatModel flatModel, List<CityModel> cityList)
        {
            FlatModel tmp = new FlatModel();
            tmp = flatModel;
            foreach (var elem in cityList)
            {
                if (elem.CityName == city)
                {
                    tmp.CityID = elem.CityID;
                    tmp.City = elem;
                    break;
                }
            }
            return tmp;
        }

    }
}
