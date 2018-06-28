using Hoangldp.Web.Core.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Hoangldp.Web.Core.Attributes
{
    public class SimpleAuthorizeAttribute : TypeFilterAttribute
    {
        public SimpleAuthorizeAttribute() : base(typeof(SimpleAuthorizeFilter))
        {
        }

        private class SimpleAuthorizeFilter : IAuthorizationFilter
        {
            private readonly IAuthenticationService _authenticationService;

            public SimpleAuthorizeFilter(IAuthenticationService authenticationService)
            {
                this._authenticationService = authenticationService;
            }

            /// <summary>
            /// Called early in the filter pipeline to confirm request is authorized
            /// </summary>
            /// <param name="filterContext">Authorization filter context</param>
            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                IUser user = _authenticationService.GetAuthenticatedUser();
                if (user == null)
                {
                    filterContext.Result = new ChallengeResult();
                }
            }
        }
    }
}
