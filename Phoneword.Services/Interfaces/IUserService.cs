using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword.Services
{
    public interface IUserService
    {
        int Authenticate(string username, string password);
    }
}
