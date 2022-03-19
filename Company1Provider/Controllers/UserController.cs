using IdentityServerCommon;
using IdentityServerCommon.Model.AuthenticationProviders;
using Microsoft.AspNetCore.Mvc;

namespace Company1Provider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private AuthProviderBase<UserPasswordAuthenticationProvider, UserAuthenticationModel, User> _authProviderBase;

        public UserController(AuthProviderBase<UserPasswordAuthenticationProvider, UserAuthenticationModel, User> authProvider)
        {
            _authProviderBase = authProvider;
        }

        [HttpPost]
        public User? Index([FromBody] UserAuthenticationModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            return _authProviderBase.AuthProvider.GetAuthenticationResponse(u => u.Username == model.UserName && u.Password == model.Password);
        }
    }
}
