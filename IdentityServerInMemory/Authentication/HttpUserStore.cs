using IdentityServerCommon.Model;
using IdentityServerCommon.Model.AuthenticationProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;

namespace IdentityServerInMemory.Authentication
{
    /// <summary>
    /// A user store using separate REST API processes for individual companies
    /// </summary>
    public class HttpUserStore
    {
        private object _providerLock = new object();
        private ICollection<AuthProviderModel> _providers = new List<AuthProviderModel>();

        /// <summary>
        /// Validates the specified user credentials
        /// </summary>
        /// <param name="userName">The user's user name</param>
        /// <param name="password">The user's password</param>
        /// <param name="companyName">The company where the user resides</param>
        /// <returns>true if the credentials are valid, false otherwise</returns>
        public async Task<bool> ValidateCredentials(string userName, string password, string companyName)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(companyName))
            {
                return false;
            }

            try
            {
                string url = _providers.FirstOrDefault(p => p.Name == companyName).CallbackUrl + "/auth";
                UserAuthenticationModel data = new UserAuthenticationModel() 
                { 
                    UserName = userName, 
                    Password = password, 
                    Company = companyName, 
                    HashAlgorithmType = HashAlgorithmType.None 
                };

                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                bool result = Boolean.Parse(await response.Content.ReadAsStringAsync());
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a User object with the specified parameters
        /// </summary>
        /// <param name="userName">The user's user name</param>
        /// <param name="password">The user's password</param>
        /// <param name="companyName">The company where the user resides</param>
        /// <returns></returns>
        public async Task<User> FindUser(string userName, string password, string companyName)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(companyName) || !_providers.Any(p => p.Name == companyName))
            {
                return null;
            }

            try
            {
                string url = _providers.FirstOrDefault(p => p.Name == companyName).CallbackUrl + "/user";
                UserAuthenticationModel data = new UserAuthenticationModel() 
                { 
                    UserName = userName, 
                    Password = password, 
                    Company = companyName, 
                    HashAlgorithmType = HashAlgorithmType.None 
                };

                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                IdentityServerCommon.Model.AuthenticationProviders.User user = JsonConvert.DeserializeObject<IdentityServerCommon.Model.AuthenticationProviders.User>(await response.Content.ReadAsStringAsync(), new IdentityServer4.Stores.Serialization.ClaimConverter());
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the distinct company names related to all registered authentication providers
        /// </summary>
        /// <returns>An IEnumerable object containing the company names</returns>
        public IEnumerable<string> GetCompanyNames()
        {
            lock (_providerLock)
            {
                return _providers.Select(u => u.Name).Distinct();
            }
        }

        /// <summary>
        /// Registers the specified authentication provider with this identity server
        /// </summary>
        /// <param name="provider">The authentication provider to be registered</param>
        public void RegisterProvider(AuthProviderModel provider)
        {
            lock (_providerLock)
            {
                _providers.Add(provider);
            }
        }

        /// <summary>
        /// Unregisters the specified authentication provider from this identity server
        /// </summary>
        /// <param name="provider">The authentication provider to be unregistered</param>
        public void UnregisterProvider(AuthProviderModel provider)
        {
            lock (_providerLock)
            {
                _providers.Remove(provider);
            }
        }
    }
}
