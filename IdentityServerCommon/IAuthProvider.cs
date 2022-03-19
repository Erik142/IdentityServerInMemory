using IdentityServerCommon.Model;

namespace IdentityServerCommon
{
    public interface IAuthProvider<T> where T : IAuthenticatable
    {
        AuthProviderModel Provider { get; }
        bool IsAuthenticationValid(T model);
    }
}