using System.Diagnostics.CodeAnalysis;

namespace Domain.ValueObjects.Common;

public abstract class ValueObject<T>([DisallowNull] T value)
{
    public readonly T Value = value ?? throw new ArgumentNullException(nameof(value));
}