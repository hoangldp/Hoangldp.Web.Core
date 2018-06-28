using Autofac;
using Hoangldp.Web.Core.Authentication;
using Hoangldp.Web.Core.Configuration;
using Hoangldp.Web.Core.Data;
using Hoangldp.Web.Core.Dependency;
using Hoangldp.Web.Core.Finder;

namespace Hoangldp.Web.Core
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CoreConfig config)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<CookieAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        }
    }
}
