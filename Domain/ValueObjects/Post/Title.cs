using Domain.ValueObjects.Common;

namespace Domain.ValueObjects.Post;

public class Title(string name)
	: ValueObject<string>(name);