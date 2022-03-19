using IdentityServerCommon.Model;

namespace IdentityServerCommon
{
    /// <summary>
    /// An interface to be implemented by authentication providers
    /// </summary>
    /// <typeparam name="T1">The type of authenticatable data received from the identity server</typeparam>
    /// <typeparam name="T2">The type of data that the provider will return to the identity server after successful authentication</typeparam>
    public interface IAuthProvider<T1, T2> where T1 : IAuthenticatable
    {
        /// <summary>
        /// The authentication provider data model used during provider (un)registration
        /// </summary>
        AuthProviderModel Provider { get; }
        /// <summary>
        /// Checks if the specified data corresponds to a valid object in the authentication provider, and therefore checks if the authentication is valid
        /// </summary>
        /// <param name="model">The data to validate in the authentication provider</param>
        /// <returns>true if the authentication data is valid, false otherwise</returns>
        public bool IsAuthenticationValid(T1 model);
        /// <summary>
        /// Retrieves the data to be sent back to the identity server
        /// </summary>
        /// <param name="predicate">Used to filter the response data</param>
        /// <returns>A data object that fulfills the predicate filter, or null if no such object exists</returns>
        public T2? GetAuthenticationResponse(Func<T2, bool> predicate);
    }
}