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
    [Table("Wiadomości")]
    public class MessagesModel
    {
        
        [Key]
        public int MessageID { get; set; }
        [MaxLength(256)]        
        public string Message { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> MessageDate { get; set; }

        [ForeignKey("UserSenderID")]
        public int UserSenderID { get; set; }

        [ForeignKey("UserReceiverID")]
        public int UserReceiverID { get; set; }
        
        public virtual UserModel UserS { get; set; }
        
        public virtual UserModel UserR { get; set; }
    }
}
