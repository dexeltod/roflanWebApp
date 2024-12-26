using Domain.ValueObjects.Common;

namespace Domain.ValueObjects.User;

public class Name(string name) : ValueObject<string>(name);