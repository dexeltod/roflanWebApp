using Domain.ValueObjects.Common;

namespace Domain.ValueObjects.User;

public class Age(int value)
	: ValueObject<int>(value);