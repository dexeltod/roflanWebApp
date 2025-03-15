using Application.Repositories;
using Application.Validation;
using FluentValidation;

namespace Infrastructure.Validation;

public class UserValidatorById : AbstractValidator<long>, IUserValidatorById
{
	private readonly IUserRepository _userRepository;

	public UserValidatorById()
	{
		RuleFor(x => x)
			.NotNull()
			.WithMessage("Id cannot be null");
	}
}