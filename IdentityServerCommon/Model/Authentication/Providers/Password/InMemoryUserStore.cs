using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model.AuthenticationProviders
{
    /// <summary>
    /// An in-memory user store, storing user objects in a Set
    /// </summary>
    public class InMemoryUserStore : IUserStore
    {
        public ISet<User> _users;
        public HashAlgorithmType HashAlgorithmType { get; private set; }

        /// <summary>
        /// Creates a new in-memory user store instance, specifying that the passwords are being hashed with a specific hashing algorithm
        /// </summary>
        /// <param name="users">The set of users used for authentication</param>
        /// <param name="hashAlgorithm">The hashing algorithm for the user passwords</param>
        public InMemoryUserStore(ISet<User> users, HashAlgorithmType hashAlgorithm)
        {
            _users = users;
            HashAlgorithmType = hashAlgorithm;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public bool DeleteUser(Func<User,bool> predicate)
        {
            User? user = _users.FirstOrDefault(predicate);

            if (user == null)
            {
                return false;
            }

            _users.Remove(user);

            return true;
        }

        public bool Exists(Func<User, bool> predicate)
        {
            return _users.Any(predicate);
        }

        public User? GetUser(Func<User, bool> predicate)
        {
            return _users.FirstOrDefault(predicate);
        }

        public void UpdateUser(User user)
        {
            if (Exists(u => u.Username == user.Username && u.Password == user.Password)) 
            {
                DeleteUser(u => u.Username == user.Username && u.Password == user.Password);
                AddUser(user);
            }
        }
    }
}
