using Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Methods
    {
        public static bool checkAdmin(string userID, ProjectContext _context)
        {
            var user = _context.User.Where(m => m.UserID.Equals(int.Parse(userID))).First();
            if (user.Login == "admin") return true;
            else return false;
        }
    }
}
