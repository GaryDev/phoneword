using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneword.rest.Authentication
{
    public interface IAuthService
    {
        int Authenticate(string userName, string password);
    }
}
