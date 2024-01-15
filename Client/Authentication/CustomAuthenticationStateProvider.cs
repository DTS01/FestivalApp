using Blazored.SessionStorage;
using FestivalApp.Client.Extensions;
using FestivalApp.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FestivalApp.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        // Service to handle the storage of user sessions
        private readonly ISessionStorageService _sessionStorage;

        // Represents an anonymous user with an empty ClaimsPrincipal
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        // Constructor to initialize the authentication state provider with a session storage service
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        // Override method to asynchronously get the current authentication state
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Retrieve the user session from the session storage
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSession>("UserSession");

                // If no user session is found, return an authentication state with an anonymous user
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                // Create a ClaimsPrincipal with user-specific claims
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }, "JwtAuth"));

                // Return the authentication state with the authenticated user's ClaimsPrincipal
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                // In case of an exception, return an authentication state with an anonymous user
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        // Method to asynchronously update the authentication state based on a provided UserSession
        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            // Check if a user session is provided
            if (userSession != null)
            {
                // Create a ClaimsPrincipal with user-specific claims
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }));

                // Update the user session expiration time and save it to the session storage
                userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
                await _sessionStorage.SaveItemEncryptedAsync("UserSession", userSession);
            }
            else
            {
                // If no user session is provided, use the anonymous ClaimsPrincipal and remove the session from storage
                claimsPrincipal = _anonymous;
                await _sessionStorage.RemoveItemAsync("UserSession");
            }

            // Notify that the authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        // Method to asynchronously get the authentication token from the current user session
        public async Task<string> GetToken()
        {
            var result = string.Empty;

            try
            {
                // Retrieve the user session from the session storage
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSession>("UserSession");

                // Check if the user session is valid and has not expired
                if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)
                    result = userSession.Token;
            }
            catch
            {
                // In case of an exception, return an empty string
            }

            // Return the authentication token
            return result;
        }
    }
}
