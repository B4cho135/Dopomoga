using AutoMapper;
using Dopomoga.API.Helpers;
using Dopomoga.Data.Entities.Identity;
using Dopomoga.Models.Dtos.Identity;
using Dopomoga.Models.Requests.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly SignInManager<UserEntity> signInManager;
        public IConfiguration Configuration { get; }
        public AccountsController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IConfiguration configuration
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            Configuration = configuration;
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
            loginResponse.User = new User()
            {
                Email = user.Email,
                Username = user.Email
            };
            loginResponse.JWT = token;
            return Ok(loginResponse);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(string email="admin@dopomoga.com", string password="Te@st1")
        {
            var newUser = new UserEntity()
            {
                PhoneNumber = "598471547",
                Email = email,
                UserName = email
            };
            var result = await userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, "admin");
                string token = JwtHelper.GenerateToken(Configuration["Jwt:Secret"], newUser, new List<string>() { "admin" });
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
