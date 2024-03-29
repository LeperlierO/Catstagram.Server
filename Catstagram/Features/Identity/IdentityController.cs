﻿using Catstagram.Data;
using Catstagram.Server.Controllers;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features.Identity
{
    public class IdentityController : ApiController
    {
        private readonly CatstagramDbContext data;
        private readonly UserManager<User> userManager;
        private IConfiguration configuration;
        public IIdentityService identityService;

        public IdentityController(UserManager<User> userManager, IConfiguration config, IIdentityService identityService, CatstagramDbContext data)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            configuration = config;
            this.data = data;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            data.Add(new Profile { UserId = user.Id, Name = model.UserName });
            await data.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
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
