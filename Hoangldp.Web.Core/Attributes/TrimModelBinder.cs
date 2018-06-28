using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hoangldp.Web.Core.Attributes
{
    public class TrimModelBinder : ComplexTypeModelBinder
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="propertyBinders">Property binders</param>
        public TrimModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders) : base(propertyBinders)
        {
        }

        /// <summary>
        ///  Updates a property in the current model
        /// </summary>
        /// <param name="bindingContext">Model binding context</param>
        /// <param name="modelName">The model name</param>
        /// <param name="propertyMetadata">The model metadata for the property to set</param>
        /// <param name="bindingResult">The binding result for the property's new value</param>
        protected override void SetProperty(ModelBindingContext bindingContext, string modelName,
            ModelMetadata propertyMetadata, ModelBindingResult bindingResult)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var valueAsString = bindingResult.Model as string;
            if (!string.IsNullOrEmpty(valueAsString))
            {
                //excluding properties with [NoTrim] attribute
                var noTrim = (propertyMetadata as DefaultModelMetadata)?.Attributes?.Attributes?.OfType<NoTrimAttribute>().Any();
                if (!noTrim.HasValue || !noTrim.Value)
                    bindingResult = ModelBindingResult.Success(valueAsString.Trim());
            }

            base.SetProperty(bindingContext, modelName, propertyMetadata, bindingResult);
        }
    }
}