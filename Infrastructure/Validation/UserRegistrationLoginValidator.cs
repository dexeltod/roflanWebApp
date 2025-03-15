using Application.DTO;
using Application.Services;
using Application.Validation;
using Domain.Models.User;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Utils;
using Utils.Resources.Localization;

namespace Infrastructure.Validation;

public class UserLoginValidator : AbstractValidator<UserDataTransferObject>, IUserLoginValidator
{
	private readonly IUserValidator _userValidator;

	public UserLoginValidator(IStringLocalizer<Localization> localizer, IUserValidator userValidator)
	{
		_userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));

		RuleFor(u => u.Email)
			.NotEmpty()
			.WithMessage(localizer[ValidationConstants.EmailEmpty]);

		RuleFor(u => u.Password)
			.NotEmpty()
			.WithMessage(localizer[ValidationConstants.PasswordEmpty]);

		RuleFor(u => u)
			.MustAsync(async (user, cancellationToken) => await IsValidLogin(user.Email, user.Password, cancellationToken))
			.WithMessage(localizer[ValidationConstants.LoginFailed]);
	}

	private async Task<bool> IsValidLogin(string email, string password, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
		if (string.IsNullOrWhiteSpace(password))
			throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

		User? user = await _userValidator.GetUserByEmail(email, cancellationToken);

		return user != null && _userValidator.CheckPassword(password, user.PasswordHash);
	}
}