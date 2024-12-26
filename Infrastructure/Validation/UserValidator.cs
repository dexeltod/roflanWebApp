using Application.DTO;
using Application.Services;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.Extensions.Localization;
using Utils;
using Utils.Resources.Localization;

namespace Infrastructure.Validation;

public class UserValidator : AbstractValidator<UserDataTransferObject>
{
    private readonly IUserService _userService;
    private const int MinimumPasswordLength = 8;
    private const int MinimumAge = 0;
    private const int MaxAge = 120;

    private const int MinEmail = 4;
    private const int MaxEmail = 30;

    private const int MinName = 2;
    private const int MaxName = 12;

    public UserValidator(IStringLocalizer<Localization> localizer, IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        RuleFor(u => u.Age).NotEmpty().WithMessage(localizer[ValidationConstants.AgeEmpty])
            .LessThan(MinimumAge).WithMessage(localizer[ValidationConstants.AgeShort])
            .GreaterThan(MaxAge).WithMessage(localizer[ValidationConstants.AgeLong]);

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

    private async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userService.IsUniqueEmail(email, cancellationToken);
    }
}