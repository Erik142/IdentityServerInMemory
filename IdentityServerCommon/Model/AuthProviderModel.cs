using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model
{
    public class AuthProviderModel
    {
        public string Name { get; private set; }
        public string CallbackUrl { get; private set; }
        public IEnumerable<AuthenticationMechanism> AuthenticationMechanisms { get; private set; }

        public AuthProviderModel(string name, string callbackUrl, IEnumerable<AuthenticationMechanism> authenticationMechanisms)
        {
            Name = name;
            CallbackUrl = callbackUrl;
            AuthenticationMechanisms = authenticationMechanisms;
        }
    }
}
