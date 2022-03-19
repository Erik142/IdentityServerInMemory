using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerCommon.Model.AuthenticationProviders
{
    /// <summary>
    /// Generic authentication provider implementation used together with the specified user storage backend
    /// </summary>
    public class UserPasswordAuthenticationProvider : IAuthProvider<UserAuthenticationModel, User>
    {
        /// <summary>
        /// The authentication provider that is related to this authentication provider
        /// </summary>
        public AuthProviderModel Provider { get; private set; }
        /// <summary>
        /// The user store (Storage backend)
        /// </summary>
        public IUserStore UserStore { get; private set; }

        /// <summary>
        /// Creates a new instance with the corresponding authentication provider model and storage backend
        /// </summary>
        /// <param name="model">The corresponding authentication provider model</param>
        /// <param name="userStore">The user store to be used with this authentication provider</param>
        public UserPasswordAuthenticationProvider(AuthProviderModel model, IUserStore userStore)
        {
            Provider = model;
            UserStore = userStore;
        }

        /// <summary>
        /// Checks if the data in the user authentication model parameter corresponds to a valid user object in the user store
        /// </summary>
        /// <param name="model">The data to check against in the user store</param>
        /// <returns>true if the user data corresponds to a valid user, false otherwise</returns>
        public bool IsAuthenticationValid(UserAuthenticationModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password) || model.HashAlgorithmType != UserStore.HashAlgorithmType)
            {
                return false;
            }

            return UserStore.Exists(u => u.Username == model.UserName && u.Password == model.Password);
        }

        /// <summary>
        /// Retrieves a user from the user store that fulfills the specified predicate filter
        /// </summary>
        /// <param name="predicate">The filter to apply when searching for a user</param>
        /// <returns>A User object that fulfills the predicate filter, or null if no such User exists in the user store</returns>
        public User? GetAuthenticationResponse(Func<User, bool> predicate)
        {
            return UserStore.GetUser(predicate);
        }
    }
}
