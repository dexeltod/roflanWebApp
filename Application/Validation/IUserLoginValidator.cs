using Application.DTO;
using FluentValidation;

namespace Application.Validation;

public interface IUserLoginValidator : IValidator<UserDataTransferObject>
{
}
