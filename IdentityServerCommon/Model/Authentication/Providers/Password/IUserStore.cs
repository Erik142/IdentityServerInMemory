using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model.AuthenticationProviders
{
    /// <summary>
    /// Interface for a generic user store
    /// </summary>
    public interface IUserStore
    {
        /// <summary>
        /// The hashing algorithm used to hash the passwords
        /// </summary>
        public HashAlgorithmType HashAlgorithmType { get; }
        /// <summary>
        /// Retrieves a user fulfilling the predicate filter
        /// </summary>
        /// <param name="predicate">The filter to apply when searching for a user</param>
        /// <returns>A user object, or null if no user is found</returns>
        public User? GetUser(Func<User,bool> predicate);
        /// <summary>
        /// Checks if a user fulfilling the predicate filter exists
        /// </summary>
        /// <param name="predicate">The filter to apply when searching for a user</param>
        /// <returns>True if a user fulfilling the predicate filter exists, false otherwise</returns>
        public bool Exists(Func<User, bool> predicate);
        /// <summary>
        /// Adds a user to the user store
        /// </summary>
        /// <param name="user">The user to be added</param>
        public void AddUser(User user);
        /// <summary>
        /// Update the specified user with new data
        /// </summary>
        /// <param name="user">The user to be updated, containing the new data</param>
        public void UpdateUser(User user);
        /// <summary>
        /// Delete the first user in the store that fulfills the specified predicate filter
        /// </summary>
        /// <param name="predicate">The filter to apply when searching for a user</param>
        /// <returns>True if the user was deleted, false otherwise</returns>
        public bool DeleteUser(Func<User, bool> predicate);
    }
}
