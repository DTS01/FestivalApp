using FestivalApp.Server.Authentication;
using FestivalApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FestivalApp.Server.Controllers
{
    // AccountController handling user account-related API requests
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // UserAccountService for managing user accounts
        private UserAccountService _userAccountService;

        // Constructor to initialize the AccountController with a UserAccountService
        public AccountController(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        // API endpoint for user login
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] LoginRequest loginRequest)
        {
            // Create an instance of JwtAuthenticationManager with the UserAccountService
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userAccountService);

            // Generate JWT token and user session based on login credentials
            var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName, loginRequest.Password);

            // Return Unauthorized if user session is null, otherwise return the user session
            if (userSession is null)
                return Unauthorized();
            else
                return userSession;
        }
    }
}
