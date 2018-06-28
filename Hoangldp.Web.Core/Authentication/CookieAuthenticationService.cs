using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hoangldp.Web.Core.Authentication
{
    /// <summary>
    /// Represents service using cookie middleware for the authentication
    /// </summary>
    public class CookieAuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private IUser _user = null;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService">User service</param>
        /// <param name="httpContextAccessor">HTTP context accessor</param>
        public CookieAuthenticationService(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this._userService = userService;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">Customer</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        public virtual async void SignIn(IUser user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //create claims for customer's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Username))
                claims.Add(new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, CookieAuthenticationDefaults.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual async void SignOut()
        {
            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>User</returns>
        public virtual IUser GetAuthenticatedUser()
        {
            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                    && claim.Issuer.Equals(CookieAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (usernameClaim != null && _user == null)
            {
                _user = _userService.GetCustomerByUsername(usernameClaim.Value);
                return _user;
            }

            return null;
        }
    }
}
