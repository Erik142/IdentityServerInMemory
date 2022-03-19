using IdentityServerCommon.Model;
using IdentityServerInMemory.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IdentityServerInMemory.Quickstart.Unregister
{
    [AllowAnonymous]
    public class UnregisterController : ControllerBase
    {
        private HttpUserStore _userStore;

        public UnregisterController(HttpUserStore userStore)
        {
            _userStore = userStore;
        }

        [HttpPost]
        public bool Index(AuthProviderModel model)
        {
            try
            {
                _userStore.UnregisterProvider(model);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
