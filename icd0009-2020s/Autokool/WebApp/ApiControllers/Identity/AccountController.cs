using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicAPI.DTO.V1.Account;
using Message = PublicAPI.DTO.v1.Message;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    ///     Controller for managing user account related actions.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAppBLL _bll;

        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        ///     Constructor for AccountController
        /// </summary>
        /// <param name="configuration">WebApplication configuration.</param>
        /// <param name="userManager">WebApplication User Manager</param>
        /// <param name="signInManager">WebApplication Sign-in Manager</param>
        /// <param name="bll">Application business logic layer</param>
        /// <param name="logger">WebApplication logger</param>
        public AccountController(
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAppBLL bll,
            ILogger<AccountController> logger)

        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _bll = bll;
        }


        /// <summary>
        ///     Authenticate user credentials and return JSON Web Token for user.
        /// </summary>
        /// <param name="dto">DTO containing login information.</param>
        /// <returns>
        ///     Response object containing JSON Web Token for authenticated User
        /// </returns>
        /// <response code="200">
        ///     User was successfully authenticated and appropriate login response containing JSON Web Token was returned.
        /// </response>
        /// <response code="403">User authentication with provided credentials failed.</response>
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] Login dto)
        {
            var appUser = await _userManager.FindByNameAsync(dto.UserName);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-api login. User with User name {dto.UserName} not found");
                return NotFound(new Message(Resources.Base.Common.ErrorMessage_UsernamePasswordMissMatch));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],                    
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );
                _logger.LogInformation($@"Token generated for user {dto.UserName}");
                return Ok(new LoginResponse
                {
                    Token = jwt,
                    Firstname = appUser.Firstname,
                    Lastname = appUser.Lastname,
                    UserName = appUser.UserName
                });
            }

            _logger.LogInformation("Web-api login. Login attempt with incorrect password.");
            return NotFound(new Message(Resources.Base.Common.ErrorMessage_UsernamePasswordMissMatch));
        }


        /// <summary>
        ///     Register new user with provided credentials into system.
        /// </summary>
        /// <param name="dto">DTO containing registration information.</param>
        /// <returns>Response object containing created user's User Name and Email</returns>
        /// <response code="200">User was successfully registered into the system.</response>
        /// <response code="403">User registration failed, see response message for details.</response>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> Register([FromBody] Register dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return BadRequest(new Message(string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.DuplicateEmail, dto.Email)));
            if (await _userManager.FindByNameAsync(dto.UserName) != null)
                return BadRequest(new Message(string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.DuplicateUserName, dto.UserName)));

            var newUser = new AppUser
            {
                UserName = dto.UserName,
                Firstname = dto.FirstName,
                Lastname = dto.LastName,
                Email = dto.Email
            };
            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded) return BadRequest(new PublicAPI.DTO.v1.Message(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.DefaultError));
            await _userManager.AddToRoleAsync(newUser, "user");
            return Ok(new RegisterResponse {Email = newUser.Email, UserName = newUser.UserName});
        }
    }
}