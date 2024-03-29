﻿using FS.FilterExpressionCreator.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FS.FilterExpressionCreator.Mvc.ModelBinders;

/// <summary>
/// ModelBinder for <see cref="EntityFilter{TEntity}"/>
/// Implements <see cref="IModelBinder" />
/// </summary>
/// <seealso cref="IModelBinder" />
public class EntityFilterSetModelBinder : IModelBinder
{
    private readonly Dictionary<Type, (ModelMetadata, IModelBinder)> _entityFilterBinders;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityFilterSetModelBinder"/> class.
    /// </summary>
    /// <param name="entityFilterBinders">The entity filter binders.</param>
    /// <autogeneratedoc />
    public EntityFilterSetModelBinder(Dictionary<Type, (ModelMetadata, IModelBinder)> entityFilterBinders)
        => this._entityFilterBinders = entityFilterBinders;

    /// <inheritdoc />
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var entityFilterSet = Activator.CreateInstance(bindingContext.ModelType);
        if (entityFilterSet == null)
            throw new InvalidOperationException($"Cannot create instance of filter set '{bindingContext.ModelType.Name}'");

        var entityFilterProperties = entityFilterSet.GetType().GetProperties();
        foreach (var entityFilter in entityFilterProperties)
        {
            var (modelMetadata, modelBinder) = _entityFilterBinders[entityFilter.PropertyType];
            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
                bindingContext.ActionContext,
                bindingContext.ValueProvider,
                modelMetadata,
                bindingInfo: null,
                bindingContext.ModelName);
            await modelBinder.BindModelAsync(newBindingContext);
            entityFilter.SetValue(entityFilterSet, newBindingContext.Result.Model);
        }

        bindingContext.Result = ModelBindingResult.Success(entityFilterSet);
    }
}