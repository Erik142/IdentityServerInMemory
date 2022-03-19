using System.Collections.Generic;
using System.Linq;

namespace IdentityServerInMemory.User
{
    public class CompanyUserStore
    {
        private ICollection<CompanyUser> _users;

        public CompanyUserStore(ICollection<CompanyUser> users)
        {
            this._users = users;
        }

        public bool ValidateCredentials(string userName, string password, string companyName)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(companyName))
            {
                return false;
            }

            return _users.Any(u => u.Username == userName && u.Password == password && u.Company == companyName);
        }

        public CompanyUser FindByUsername(string userName, string companyName)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(companyName) || !_users.Any(u => u.Username == userName && u.Company == companyName))
            {
                return null;
            }

            return _users.FirstOrDefault(u => u.Username == userName && u.Company == companyName);
        }

        public IEnumerable<string> GetCompanyNames()
        {
            return _users.Select(u => u.Company).Distinct();
        }
    }
}
