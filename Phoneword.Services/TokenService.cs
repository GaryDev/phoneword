using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoneword.Entities;
using Phoneword.DataModel;
using Phoneword.DataModel.UnitOfWork;
using System.Configuration;

namespace Phoneword.Services
{
    public class TokenService : ITokenService
    {
        private readonly UnitOfWork _unitOfWork;
        private static readonly string _authTokenExpiry = ConfigurationManager.AppSettings["AuthTokenExpiry"];

        /// <summary>
        /// Public constructor.
        /// </summary>
        public TokenService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool DeleteByUserId(int userId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.User1.UserId == userId);
            _unitOfWork.Save();

            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.User1.UserId == userId).Any();
            return !isNotDeleted;
        }

        public TokenEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(_authTokenExpiry));

            var tokendomain = new Token
            {
                UniqueId = Guid.NewGuid(),
                User = GetUserUniqueId(userId),
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };

            _unitOfWork.TokenRepository.Insert(tokendomain);
            _unitOfWork.Save();

            var tokenModel = new TokenEntity()
            {
                UserId = userId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                AuthToken = token
            };
            return tokenModel;
        }

        public bool Kill(string tokenId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.AuthToken == tokenId);
            _unitOfWork.Save();
            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.AuthToken == tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        public bool ValidateToken(string tokenId)
        {
            var token = _unitOfWork.TokenRepository.Get(t => t.AuthToken == tokenId && t.ExpiresOn > DateTime.Now);
            var now = DateTime.Now;
            if (token != null && !(now > token.ExpiresOn))
            {
                token.ExpiresOn = now.AddSeconds(Convert.ToDouble(_authTokenExpiry));
                _unitOfWork.TokenRepository.Update(token);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        private Guid GetUserUniqueId(int userId)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserId == userId);
            return user.UniqueId;
        }
    }
}
