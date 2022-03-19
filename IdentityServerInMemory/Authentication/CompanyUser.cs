using IdentityModel;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerInMemory.User
{
    // Copy pasted from TestUser to prove that inheritance is not necessary
    public class CompanyUser
    {
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
