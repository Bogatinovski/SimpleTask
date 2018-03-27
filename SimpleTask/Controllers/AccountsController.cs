using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountsController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
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

            //await appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
            //await appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}