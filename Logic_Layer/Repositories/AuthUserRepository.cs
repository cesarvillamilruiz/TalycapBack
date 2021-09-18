using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.DTO;
using Logic_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Repositories
{
    public class AuthUserRepository : GenericRepository<User>, IAuthUser
    {
        private readonly DataContext context;

        public AuthUserRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> Validate_Login(LoginAuthUserDTO model)
        {
            var authUser = await this.context.User.
                           Where(a => a.userName == model.User && a.password == model.Password).
                           FirstAsync();

            if (authUser == null) return null;
            return authUser;
        }
    }
}
