using Domain.ValueObjects.Common;

namespace Domain.ValueObjects.User;

public class Posts<T>(IReadOnlyCollection<T> name) : ValueObject<IReadOnlyCollection<T>>(name);