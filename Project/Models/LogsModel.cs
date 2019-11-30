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

namespace Project.Models
{
    [Table("Logi")]
    public class LogsModel
    {
        
        [Key]
        public int LogID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> MessageDate { get; set; }

        [MaxLength(50)]
        public string Log { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }   
        
        public virtual UserModel UserR { get; set; }

    }
}
