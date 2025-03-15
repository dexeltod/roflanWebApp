using Application.DTO;
using FluentValidation;

namespace Application.Validation;

public interface IUserRegistrationValidator : IValidator<UserDataTransferObject>
{
	Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken);
}

public interface IUserValidatorById : IValidator<long>
{
}