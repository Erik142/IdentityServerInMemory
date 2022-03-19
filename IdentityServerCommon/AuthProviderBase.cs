using IdentityServerCommon.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon
{
    public class AuthProviderBase<T> where T : IAuthenticatable
    {
        private IAuthProvider<T> _authProvider;
        private string serverUrl;

        public AuthProviderBase(IAuthProvider<T> authProvider, string serverUrl) 
        {
            this._authProvider = authProvider;
            this.serverUrl = serverUrl;
        }

        public async Task Register()
        {
            await PerformServerPostRequest("register");
        }

        public async Task Unregister()
        {
            await PerformServerPostRequest("unregister");
        }

        private async Task PerformServerPostRequest(string endpoint)
        {
            var client = new HttpClient();
            await client.PostAsync(serverUrl + "/" + endpoint, new StringContent(JsonConvert.SerializeObject(_authProvider.Provider), Encoding.UTF8, "application/json"));
        }

        public bool Authenticate(T model)
        {
            return _authProvider.IsAuthenticationValid(model);
        }
    }
}
