using Application.DTO;
using Application.Services;
using Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Utils;
using Utils.Resources.Localization;

namespace Infrastructure.Validation;

public class UserRegistrationValidator : AbstractValidator<UserDataTransferObject>, IUserRegistrationValidator
{
	private const int MinimumPasswordLength = 8;
	private const int MinimumAge = 0;
	private const int MaxAge = 120;

	private const int MinEmail = 2;
	private const int MaxEmail = 320;

	private const int MinName = 2;
	private const int MaxName = 12;
	private readonly IUserValidator _userValidator;

	public UserRegistrationValidator(IStringLocalizer<Localization> localizer, IUserValidator userValidator)
	{
		_userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));

		RuleFor(u => u.Age).NotEmpty().WithMessage(localizer[ValidationConstants.AgeEmpty])
			.GreaterThanOrEqualTo(MinimumAge).WithMessage(localizer[ValidationConstants.AgeShort])
			.LessThanOrEqualTo(MaxAge).WithMessage(localizer[ValidationConstants.AgeLong]);

		RuleFor(u => u.Name)
			.NotEmpty().WithMessage(localizer[ValidationConstants.NameEmpty])
			.MinimumLength(MinName).WithMessage(localizer[ValidationConstants.NameShort])
			.MaximumLength(MaxName).WithMessage(localizer[ValidationConstants.NameLong]);

		RuleFor(u => u.Password)
			.NotEmpty().WithMessage(localizer[ValidationConstants.PasswordEmpty])
			.MinimumLength(MinimumPasswordLength).WithMessage(localizer[ValidationConstants.PasswordShort]);

		RuleFor(u => u.Email)
			.NotEmpty().WithMessage(localizer[ValidationConstants.EmailEmpty])
			.MinimumLength(MinEmail).WithMessage(localizer[ValidationConstants.EmailShort])
			.MaximumLength(MaxEmail).WithMessage(localizer[ValidationConstants.EmailLong]).EmailAddress()
			.MustAsync(IsUniqueEmail).WithMessage(localizer[ValidationConstants.EmailExists]);
	}

	public async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken) =>
		!await _userValidator.IsEmailExists(email, cancellationToken);
}