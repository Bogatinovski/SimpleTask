using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleTask.AppSettings;
using SimpleTask.Data;
using SimpleTask.Helpers;
using SimpleTask.Models;
using SimpleTask.ViewModels;

namespace SimpleTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private IdentityServerSettings identityServerSettings;

        public AccountsController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext, IOptions<IdentityServerSettings> identityServerSettings)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
            this.identityServerSettings = identityServerSettings.Value;
        }

        // POST api/Accounts/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser userIdentity = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Errors.AddErrorsToModelState(result, ModelState));
            }

            return Ok(userIdentity);
        }

        // POST api/Accounts/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel credentials)
        {
            var disco = await DiscoveryClient.GetAsync(identityServerSettings.AuthorityEndpoint);

            if(disco.IsError)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username of password", ModelState));
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(credentials.Email, credentials.Password, "api1");

            if (tokenResponse.IsError)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", tokenResponse.Error, ModelState));
            }

            return Ok(new { Token = tokenResponse.AccessToken });
        }
    }
}