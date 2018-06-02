using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKids.Models.Domain
{
    public class UserBase : IUserAuthData
    {
        public int Id
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public IEnumerable<string> Roles
        {
            get; set;
        }

        public string RoleId { get; set; }
    }
}
