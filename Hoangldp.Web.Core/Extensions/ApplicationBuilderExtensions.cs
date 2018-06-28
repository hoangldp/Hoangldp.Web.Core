using Hoangldp.Web.Core.Authentication;
using Hoangldp.Web.Core.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Hoangldp.Web.Core.Extensions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        /// <param name="env"></param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application, IHostingEnvironment env)
        {
            EngineContext.Current.ConfigureRequestPipeline(application, env);
        }

        /// <summary>
        /// Adds the authentication middleware, which enables authentication capabilities.
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseCoreAuthentication(this IApplicationBuilder application)
        {
            application.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
