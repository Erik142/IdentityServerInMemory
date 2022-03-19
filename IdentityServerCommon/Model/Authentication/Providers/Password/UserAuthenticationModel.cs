using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model.AuthenticationProviders
{
    /// <summary>
    /// User authentication model used to send data between the identity server and the authentication provider when an authentication request is being made
    /// </summary>
    public class UserAuthenticationModel : IAuthenticatable
    {
        /// <summary>
        /// The authentication mechanism for this model
        /// </summary>
        public AuthenticationMechanism AuthenticationMechanism { get; } = AuthenticationMechanism.UserPassword;
        /// <summary>
        /// The hashing algorithm used to hash the passwords
        /// </summary>
        public HashAlgorithmType HashAlgorithmType { get; set; }
        /// <summary>
        /// The company name
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// The user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The user's password, hashed according to the specified hashing algorithm
        /// </summary>
        public string Password { get; set; }
    }
}
