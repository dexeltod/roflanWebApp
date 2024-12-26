using Domain.ValueObjects.Common;

namespace Domain.ValueObjects.Post;

public class Content(string name) : ValueObject<string>(name);