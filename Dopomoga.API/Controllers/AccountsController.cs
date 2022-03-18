using API.Enums;
using AutoMapper;
using Core.Entities.Users;
using Dopomoga.Data.Entities.Identity;
using Dopomoga.Models.Requests.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Requests;
using Models.Responses;
using Models.Users;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private UserManager<UserEntity> userManager;
        private IMapper mapper;
        private readonly SignInManager<UserEntity> signInManager;
        public IConfiguration Configuration { get; }
        public AccountsController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            Configuration = configuration;
            this.mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            UserEntity user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return BadRequest("Invalid Phone/Password");
            }

            var logUserIn = await signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);

            if (!logUserIn.Succeeded)
            {
                return BadRequest();
            }
            var userRoles = await userManager.GetRolesAsync(user);
            string token = JwtHelper.GenerateToken(Configuration["JWT:Secret"], user, userRoles);
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.User = mapper.Map<User>(user);
            loginResponse.User.Roles = await userManager.GetRolesAsync(user);
            loginResponse.JWT = token;
            return Ok(loginResponse);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegistrantRoleFilter role, RegisterRequest registerRequest)
        {
            var newUser = new UserEntity()
            {
                PhoneNumber = registerRequest.PhoneNumber,
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
                Name = registerRequest.Name,
                Surname = registerRequest.Surname
            };
            var result = await userManager.CreateAsync(newUser, registerRequest.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, role.ToString());
                string token = JwtHelper.GenerateToken(Configuration["Jwt:Secret"], newUser, new List<string>() { role.ToString() });
                return Ok(token);
            }
            return BadRequest();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured during sign out process - {ex.Message}");
            }
        }
    }
}
