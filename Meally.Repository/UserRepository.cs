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
        private readonly StoreContext _context;

        public UserRepository(StoreContext context)
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
