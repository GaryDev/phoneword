using Phoneword.DataModel.UnitOfWork;
using Phoneword.Services.ErrorHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Authenticate(string username, string password)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Get(u => u.UserName == username && u.Password == password);

                if (user != null && user.UserId > 0)
                    return user.UserId;

                return 0;
            }
            catch (Exception ex)
            {
                throw new ApiException { ErrorCode = 999, ErrorDescription = ex.Message, HttpStatus = HttpStatusCode.InternalServerError };
            }
        }
    }
}
