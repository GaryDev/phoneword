
using Phoneword.DataModel;
using Phoneword.Util;

namespace Phoneword.Command
{
    public partial class AppCommand
    {
        private WebApiClient _client;

        public AppCommand()
        {
            _client = new WebApiClient();
        }

        public AppCommand(string token) : this()
        {
            _client.SessionToken = token;
        }

        public async void DoLogin(UserLogin user, OnSuccessHandler success, OnFailureHandler failure)
        {
            await _client.DoPost("api/auth/login", user, success, failure);
        }
    }
}