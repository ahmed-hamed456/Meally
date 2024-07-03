using Meally.core.Repository.Contract;
using Meally.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppIdentityDbContext _context;

        public UserRepository(AppIdentityDbContext context)
        {
            _context = context;
        }
        public void RemoveCallBack(object state)
        {
            lock (_context)
            {

            }
        }
    }
}
