using Application.DTO;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Utils;
using Utils.Resources.Localization;

namespace Infrastructure.Validation;

public class PostValidator : AbstractValidator<PostDataTransferObject>
{
	public PostValidator(IStringLocalizer<Localization> localizer)
	{
		RuleFor(u => u.AuthorId).NotEmpty().WithMessage(localizer[ValidationConstants.NameEmpty]);

		RuleFor(u => u.Title)
			.NotEmpty()
			.WithMessage(localizer[ValidationConstants.TitleEmpty])
			.MinimumLength(2)
			.WithMessage(localizer[ValidationConstants.TitleShort])
			.MaximumLength(20)
			.WithMessage(localizer[ValidationConstants.TitleLong]);

		RuleFor(u => u.Content)
			.NotEmpty()
			.WithMessage(localizer[ValidationConstants.ContentEmpty])
			.MinimumLength(2)
			.WithMessage(localizer[ValidationConstants.ContentShort])
			.MaximumLength(400)
			.WithMessage(localizer[ValidationConstants.ContentLong]);
	}
}