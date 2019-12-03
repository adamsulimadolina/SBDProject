using Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Methods
    {
        public static bool checkAdmin(int userID, ProjectContext _context)
        {
            var user = _context.User.Where(m => m.UserID.Equals(userID)).First();
            if (user.Login == "admin") return true;
            return false;
        }

        public static bool checkOwner(int owner_id, ProjectContext _context, int user_id)
        {
            var owner = _context.Owners.Where(m => m.OwnerID.Equals(owner_id)).First();
            if (owner.UserID.Equals(user_id)) return true;
            return false;
        }
    }
}
