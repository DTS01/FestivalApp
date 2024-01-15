using FestivalApp.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FestivalApp.Server.Authentication
{
    public class JwtAuthenticationManager
    {
        // Constant for JWT security key
        public const string JWT_SECURITY_kEY = "yGiBjDwgpIUWcDwEsjhAUilhJkGuFtDjhniytUIfHefjOsIHyULmVxz";

        // Constant for JWT token validity duration in minutes
        private const int JWT_TOKEN_VALIDITY_MINS = 20;

        // User account service for validating user credentials
        private UserAccountService _userAccountService;

        // Constructor to initialize the JwtAuthenticationManager with a UserAccountService
        public JwtAuthenticationManager(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        // Method to generate a JWT token based on provided username and password
        public UserSession? GenerateJwtToken(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;

            // Validating user credentials
            var userAccount = _userAccountService.GetUserAccountByUserName(userName);
            if (userAccount == null || userAccount.Password != password)
                return null;

            // Generating JWT Token
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_kEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.Role, userAccount.Role)
            });
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            // Return the User Session object
            var userSession = new UserSession
            {
                UserName = userAccount.UserName,
                Role = userAccount.Role,
                Token = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
            return userSession;
        }
    }
}
