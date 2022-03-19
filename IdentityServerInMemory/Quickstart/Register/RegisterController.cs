using IdentityServerCommon.Model;
using IdentityServerInMemory.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IdentityServerInMemory.Quickstart.Register
{
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private HttpUserStore _userStore;

        public RegisterController(HttpUserStore userStore)
        {
            _userStore = userStore;
        }

        [HttpPost]
        public bool Index([FromBody] AuthProviderModel model)
        {
            try
            {
                _userStore.RegisterProvider(model);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
