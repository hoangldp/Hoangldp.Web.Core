using Hoangldp.Web.Core.Engine;
using Hoangldp.Web.Core.Finder;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hoangldp.Web.Core.Data
{
    public abstract class DatabaseContext : DbContext, IDataContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var listTypeBuilder = EngineContext.Current.Resolve<ITypeFinder>().FindClassesOfType<IModelBuilder>();
            foreach (Type typeBuilder in listTypeBuilder)
            {
                IModelBuilder builder = (IModelBuilder)Activator.CreateInstance(typeBuilder, modelBuilder);
                builder.Build();
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
