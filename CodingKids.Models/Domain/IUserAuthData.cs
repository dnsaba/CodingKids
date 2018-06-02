using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKids.Models.Domain
{
    public interface IUserAuthData
    {
        int Id { get; }
        string UserName { get; }
        IEnumerable<string> Roles { get; }
        string RoleId { get; }
    }
}
