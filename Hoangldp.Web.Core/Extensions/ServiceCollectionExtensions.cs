using Hoangldp.Web.Core.Attributes;
using Hoangldp.Web.Core.Authentication;
using Hoangldp.Web.Core.Configuration;
using Hoangldp.Web.Core.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;

namespace Hoangldp.Web.Core.Extensions
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static CoreConfig _coreConfig;
        private static SecurityConfig _securityConfig;

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        /// <returns>Configured service provider</returns>
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));
            _coreConfig = services.ConfigureStartupConfig<CoreConfig>(configuration.GetSection("Core"));
            _securityConfig = services.ConfigureStartupConfig<SecurityConfig>(configuration.GetSection("Core").GetSection("Security"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services);
            var serviceProvider = engine.ConfigureServices(services, configuration);

            return serviceProvider;
        }

        /// <summary>
        /// Create, bind and register as service the specified configuration parameters
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds services required for anti-forgery support
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddAntiForgery(this IServiceCollection services)
        {
            //override cookie name
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = ".Hoangldp.Antiforgery";

                //whether to allow the use of anti-forgery cookies from SSL protected page on the other store pages which are not
                options.Cookie.SecurePolicy = EngineContext.Current.Resolve<SecurityConfig>().ForceSslForAllPages
                    ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            });
        }

        /// <summary>
        /// Adds services required for application session state
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = ".Hoangldp.Session";
                options.Cookie.HttpOnly = true;

                //whether to allow the use of session values from SSL protected page on the other store pages which are not
                options.Cookie.SecurePolicy = EngineContext.Current.Resolve<SecurityConfig>().ForceSslForAllPages
                    ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            });
        }

        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static IMvcBuilder AddCoreMvc(this IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddMvc();

            //use session temp data provider
            mvcBuilder.AddSessionStateTempDataProvider();

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //add custom model binder provider (to the top of the provider list)
            mvcBuilder.AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new TrimmingModelBinderProvider()));

            return mvcBuilder;
        }

        /// <summary>
        /// Adds authentication service
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddCoreAuthentication(this IServiceCollection services)
        {
            //set default authentication schemes
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.ExternalAuthenticationScheme;
            });

            //add main cookie authentication
            authenticationBuilder.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.AuthenticationScheme;
                options.Cookie.HttpOnly = true;
                options.LoginPath = CookieAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath;

                //whether to allow the use of authentication cookies from SSL protected page on the other store pages which are not
                options.Cookie.SecurePolicy = _securityConfig.ForceSslForAllPages
                    ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            });

            //add external authentication
            authenticationBuilder.AddCookie(CookieAuthenticationDefaults.ExternalAuthenticationScheme, options =>
            {
                options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.ExternalAuthenticationScheme;
                options.Cookie.HttpOnly = true;
                options.LoginPath = CookieAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath;

                //whether to allow the use of authentication cookies from SSL protected page on the other store pages which are not
                options.Cookie.SecurePolicy = _securityConfig.ForceSslForAllPages
                    ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.None;
            });

            //register and configure external authentication plugins now
            //var typeFinder = new WebAppTypeFinder(_coreConfig);
            //var externalAuthConfigurations = typeFinder.FindClassesOfType<IExternalAuthenticationRegistrar>();
            //var externalAuthInstances = externalAuthConfigurations
            //    .Select(x => (IExternalAuthenticationRegistrar)Activator.CreateInstance(x));

            //foreach (var instance in externalAuthInstances)
            //    instance.Configure(authenticationBuilder);
        }
    }
}
