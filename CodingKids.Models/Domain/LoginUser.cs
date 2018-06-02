using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKids.Models.Domain
{
    public class LoginUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        //public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string HashPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RoleId { get; set; }
    }
}
