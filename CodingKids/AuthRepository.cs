using CodingKids.Models.Domain;
using CodingKids.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CodingKids
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;
        private UserService _userService = new UserService();

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public int RegisterUser(NewUser model)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = model.UserName
            };

            var result = _userService.Create(model);

            return result;
        }

        //public async Task<LoginUser> FindUser(string userName, string password)
        //{
        //    LoginUser user = _userService.LogIn(userName, password);

        //    return user;
        //}

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}