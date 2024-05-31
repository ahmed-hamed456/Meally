using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Repository.Contract
{
    public interface IUserRepository
    {
        void RemoveCallBack(object state);
    }
}
