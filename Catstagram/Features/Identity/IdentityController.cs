using Catstagram.Server.Controllers;
using Catstagram.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features.Identity
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private IConfiguration configuration;
        public IIdentityService identityService;

        public IdentityController(UserManager<User> userManager, IConfiguration config, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            configuration = config;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null) return Unauthorized();

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid) return Unauthorized();

            return new LoginResponseModel { 
                Token = this.identityService.GenerateJwtToken(user.Id, user.UserName, configuration.GetValue<string>("Jwt:Key")) 
            };
        }
    }
}
