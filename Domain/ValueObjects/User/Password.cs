using Domain.ValueObjects.Common;

namespace Domain.ValueObjects.User;

public class Password(string value)
	: ValueObject<string>(value)
{
};