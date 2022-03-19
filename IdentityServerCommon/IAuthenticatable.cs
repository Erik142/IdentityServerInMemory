using IdentityServerCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon
{
    /// <summary>
    /// A simple interface used to specify model classes that can be used as data classes to authenticate with a specific authentication mechanism
    /// </summary>
    public interface IAuthenticatable
    {
        /// <summary>
        /// The authentication mechanism used for this authenticatable object
        /// </summary>
        public AuthenticationMechanism AuthenticationMechanism { get; }
    }
}
