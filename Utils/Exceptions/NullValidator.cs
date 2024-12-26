namespace Utils.Exceptions;

public static class NullValidator
{
    public static void ValidateNull(object? obj) =>
        ArgumentNullException.ThrowIfNull(obj);

    public static void ValidateEmptyArray(object[]? obj)
    {
        if (obj == null || obj.Length == 0)
            ArgumentNullException.ThrowIfNull(obj);
    }
}