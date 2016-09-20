using Phoneword.ViewModel;
using System;

namespace phoneword.rest.Actions
{
    public class AuthenticationAction
    {
        public static UserLoginData Login(UserLogin user)
        {
            if (user != null && user.Email == "test@example.com" && user.Password == "tester")
                return new UserLoginData { Username = "Tester", SessionToken = Guid.NewGuid().ToString() };
            else
                throw new Exception("Invalid user name or password");
        }
    }
}