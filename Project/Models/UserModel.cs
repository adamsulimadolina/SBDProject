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
    [Table("User")]
    public class UserModel
    {       
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Login jest wymagany. ")]
        [DisplayName("Nazwa użytkownika")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Hasła różnią się.")]
        [DataType(DataType.Password)]
        [DisplayName("Powtórz hasło")]
        public string VerifyPassword { get; set; }

        public int getUserID()
        {
            return UserID;
        }
        public string getLogin()
        {
            return Login;
        }

    }
}
