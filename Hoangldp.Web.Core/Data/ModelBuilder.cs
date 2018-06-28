using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hoangldp.Web.Core.Data
{
    public abstract class ModelBuilder<TEntity> : IModelBuilder where TEntity : class, IEntity, new()
    {
        private readonly ModelBuilder _modelBuilder;

        public ModelBuilder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Build()
        {
            OnModelCreating(_modelBuilder.Entity<TEntity>());
        }

        public abstract void OnModelCreating(EntityTypeBuilder<TEntity> entityTypeBuilder);
    }
}
