﻿using FS.FilterExpressionCreator.Abstractions.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Reflection;

namespace FS.FilterExpressionCreator.Mvc.ModelBinders;

/// <summary>
/// Class EntityFilterModelBinderProvider.
/// Implements <see cref="IModelBinderProvider" />
/// </summary>
/// <seealso cref="IModelBinderProvider" />
/// <autogeneratedoc />
public class EntityFilterSetModelBinderProvider : IModelBinderProvider
{
    /// <inheritdoc />
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var entityFilterSetAttribute = context.Metadata.ModelType.GetCustomAttribute<EntityFilterSetAttribute>();
        if (entityFilterSetAttribute == null)
            return null;

        var entityFilterBinders = context.Metadata.ModelType
            .GetProperties()
            .Select(property => GetModelBinder(property, context))
            .ToDictionary(x => x.Type, x => (x.Metadata, x.Binder));

        return new EntityFilterSetModelBinder(entityFilterBinders);
    }

    private static (Type Type, ModelMetadata Metadata, IModelBinder Binder) GetModelBinder(PropertyInfo property, ModelBinderProviderContext context)
    {
        var modelMetadata = context.MetadataProvider.GetMetadataForType(property.PropertyType);
        var modelBinder = context.CreateBinder(modelMetadata);
        return (property.PropertyType, modelMetadata, modelBinder);
    }
}