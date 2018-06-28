using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace Hoangldp.Web.Core.Attributes
{
    /// <summary>
    /// Represents model binder provider for the creating ModelBinder
    /// </summary>
    public class TrimmingModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Creates a nop model binder based on passed context
        /// </summary>
        /// <param name="context">Model binder provider context</param>
        /// <returns>Model binder</returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            //use NopModelBinder as a ComplexTypeModelBinder for BaseNopModel
            if (context.Metadata.IsComplexType && !context.Metadata.IsCollectionType)
            {
                //create binders for all model properties
                var propertyBinders = context.Metadata.Properties
                    .ToDictionary(modelProperty => modelProperty, modelProperty => context.CreateBinder(modelProperty));

                return new TrimModelBinder(propertyBinders);
            }

            //or return null to further search for a suitable binder
            return null;
        }
    }
}
