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
    [Table("Logi")]
    public class LogsModel
    {
        
        [Key]
        public int LogID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DisplayName("Data")]
        public Nullable<System.DateTime> MessageDate { get; set; }

        [MaxLength(50)]
        [DisplayName("Wiadomość")]
        public string Log { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }   
        
        public virtual UserModel UserR { get; set; }

    }
}
