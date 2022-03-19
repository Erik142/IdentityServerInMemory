using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model
{
    /// <summary>
    /// Data model for an authentication provider. This is what is being passed between the authentication provider and the identity server during authentication provider registration.
    /// </summary>
    public class AuthProviderModel
    {
        /// <summary>
        /// The Authentication provider name, e.g. the company name
        /// </summary>
        public string Name { get;  set; }
        /// <summary>
        /// The callback URL for this authentication provider. Used to call API endpoints in the authentication provider
        /// </summary>
        public string CallbackUrl { get;  set; }
        /// <summary>
        /// The specific authentication mechanisms that this authentication provider supports
        /// </summary>
        public IEnumerable<AuthenticationMechanism> AuthenticationMechanisms { get;  set; }
    }
}
