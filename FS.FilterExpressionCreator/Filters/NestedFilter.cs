using System;

namespace FS.FilterExpressionCreator.Filters;

internal class NestedFilter
{
    public string PropertyName { get; }

    [Obsolete("Use 'Schick.Plainquire.Filter.Filters.NestedFilter.EntityFilter' instead.")]
    public EntityFilter EntityFilter { get; }

    [Obsolete("Use 'Schick.Plainquire.Filter.Filters.NestedFilter' instead.")]
    public NestedFilter(string propertyName, EntityFilter? entityFilter)
    {
        PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        EntityFilter = entityFilter ?? new EntityFilter();
    }
}