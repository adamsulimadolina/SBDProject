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
    public class MessagesModel
    {
        [Required]
        public int MessageID { get; set; }
        [MaxLength(256)]
        public string Message { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> MessageDate { get; set; }

        public int UserSenderID { get; set; }

        public int UserReceiverID { get; set; }

        [ForeignKey("UserSenderID")]
        public virtual UserModel UserS { get; set; }

        [ForeignKey("UserReceiverID")]
        public virtual UserModel UserR { get; set; }
    }
}
