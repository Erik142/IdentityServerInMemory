using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model.AuthenticationProviders
{
    /// <summary>
    /// A user data model used during token creation
    /// </summary>
    public class User
    {
        //
        // Summary: Gets or sets the user's company name
        //
        public string Company
        {
            get;
            set;
        }
        //
        // Summary:
        //     Gets or sets the subject identifier.
        public string SubjectId
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets or sets the username.
        public string Username
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets or sets the password.
        public string Password
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets or sets the provider name.
        public string ProviderName
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets or sets the provider subject identifier.
        public string ProviderSubjectId
        {
            get;
            set;
        }

        //
        // Summary:
        //     Gets or sets if the user is active.
        public bool IsActive
        {
            get;
            set;
        } = true;


        //
        // Summary:
        //     Gets or sets the claims.
        public ICollection<Claim> Claims
        {
            get;
            set;
        } = new HashSet<Claim>(new ClaimComparer());
    }
}
