using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model
{
    public enum AuthenticationMechanism
    {
        Unknown,
        UserPassword,
        Certificate,
        BankId
    }
}
