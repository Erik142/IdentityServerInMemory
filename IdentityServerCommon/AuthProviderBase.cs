using IdentityServerCommon.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon
{
    /// <summary>
    /// Helper class used to make it easier to implement new authentication providers in ASP.NET Core
    /// </summary>
    /// <typeparam name="T1">The type of Authentication Provider to be used</typeparam>
    /// <typeparam name="T2">The type of authenticatable data that this provider will receive from the identity server</typeparam>
    /// <typeparam name="T3">The type for the data class that will be returned to the identity server during authentication</typeparam>
    public class AuthProviderBase<T1, T2, T3> where T1 : IAuthProvider<T2, T3> where T2 : IAuthenticatable
    {
        /// <summary>
        /// The authentication provider that will be used
        /// </summary>
        public T1 AuthProvider { get; private set; }
        /// <summary>
        /// The base url for the identity server
        /// </summary>
        private string serverUrl;

        /// <summary>
        /// Creates a new instance with the specified authentication provider and identity server url
        /// </summary>
        /// <param name="authProvider">The authentication provider that will be used</param>
        /// <param name="serverUrl">The base url to the identity server</param>
        public AuthProviderBase(T1 authProvider, string serverUrl) 
        {
            this.AuthProvider = authProvider;
            this.serverUrl = serverUrl;
        }

        /// <summary>
        /// Registers the authentication provider with the identity server
        /// </summary>
        /// <returns>true if the registration succeeded, false otherwise</returns>
        public async Task<bool> RegisterAsync()
        {
            return await PerformServerPostRequest("register");
        }

        /// <summary>
        /// Unregisters the authentication provider from the identity server
        /// </summary>
        /// <returns>true if the unregistration succeeded, false otherwise</returns>
        public async Task<bool> UnregisterAsync()
        {
            return await PerformServerPostRequest("unregister");
        }

        /// <summary>
        /// Sends the authentication provider object to the specified endpoint of the identity server using a HTTP Post request
        /// </summary>
        /// <param name="endpoint">The endpoint to post to</param>
        /// <returns>true if the identity server responded with an HTTP 200 response code, false otherwise</returns>
        private async Task<bool> PerformServerPostRequest(string endpoint)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(serverUrl + "/" + endpoint, new StringContent(JsonConvert.SerializeObject(AuthProvider.Provider), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Authenticates the given authenticatable model with the authentication provider
        /// </summary>
        /// <param name="model">The data used for authentication with the authentication provider</param>
        /// <returns>true if the authentication data is valid and the authentication succeeded, false otherwise</returns>
        public bool Authenticate(T2 model)
        {
            return AuthProvider.IsAuthenticationValid(model);
        }
    }
}
