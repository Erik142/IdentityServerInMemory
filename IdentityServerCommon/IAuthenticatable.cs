using IdentityServerCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon
{
    public interface IAuthenticatable
    {
        public AuthenticationMechanism AuthenticationMechanism { get; }
    }
}
