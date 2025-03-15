namespace Domain.ValueObjects.Interfaces;

public interface IValidatable<in T>
{
	public bool IsValid { get; }
	public bool Validate(T value);
}