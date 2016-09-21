using Phoneword.DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var user = _unitOfWork.UserRepository.Get(u => u.UserName == username && u.Password == password);

            if (user != null && user.UserId > 0)
                return user.UserId;

            return 0;
        }
    }
}
