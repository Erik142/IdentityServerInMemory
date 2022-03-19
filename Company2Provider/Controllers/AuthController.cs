using IdentityServerCommon;
using IdentityServerCommon.Model.AuthenticationProviders;
using Microsoft.AspNetCore.Mvc;

namespace Company2Provider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private AuthProviderBase<UserPasswordAuthenticationProvider, UserAuthenticationModel, User> _authProviderBase;

        public AuthController(AuthProviderBase<UserPasswordAuthenticationProvider, UserAuthenticationModel, User> authProvider)
        {
            _authProviderBase = authProvider;
        }

        [HttpPost]
        public bool Index([FromBody] UserAuthenticationModel model)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            return _authProviderBase.Authenticate(model);
        }
    }
}
