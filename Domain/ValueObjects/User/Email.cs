using Domain.ValueObjects.Common;

namespace Domain.ValueObjects.User;

public class Email(string value) : ValueObject<string>(value);