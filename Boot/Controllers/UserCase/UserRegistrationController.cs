using Application.DTO;
using Application.Repositories;
using Application.Validation;
using Domain.Models.User;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Boot.Controllers.UserCase;

public class UserRegistrationController(
	ILogger<UserRegistrationController> logger,
	IUserRepository userRepository,
	IUserRegistrationValidator userRegistrationValidator,
	IUserValidatorById userValidatorById
) : ControllerBase
{
	private readonly ILogger<UserRegistrationController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

	private readonly IUserRegistrationValidator _userRegistrationValidator =
		userRegistrationValidator ?? throw new ArgumentNullException(nameof(userRegistrationValidator));

	private readonly IUserRepository _userRepository =
		userRepository ?? throw new ArgumentNullException(nameof(userRepository));

	private readonly IUserValidatorById _userValidator =
		userValidatorById ?? throw new ArgumentNullException(nameof(userRegistrationValidator));

	[HttpDelete]
	public async Task<IActionResult> DeleteUser(long id, CancellationToken cancellationToken)
	{
		await _userRepository.DeleteUser(id, cancellationToken);
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> Get(long[] ids, CancellationToken cancellationToken)
	{
		List<User?> users = await _userRepository.GetUsers(ids, cancellationToken);

		return Ok(users);
	}

	[HttpGet]
	public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
	{
		User? user = await _userRepository.GetUserById(id, cancellationToken);

		if (user == null)
			return BadRequest();

		return Ok(user);
	}

	[HttpGet("get-user")]
	public async Task<IActionResult> GetUser(long id, CancellationToken cancellationToken)
	{
		ValidationResult validation = await ValidateAsync(id, cancellationToken);

		if (validation.IsValid == false)
			return BadRequest(validation.Errors);

		User? user = await _userRepository.GetUserById(id, cancellationToken);

		return Ok(user);
	}

	[HttpPost("register-user")]
	public async Task<IActionResult> Register(
		[FromBody] UserDataTransferObject userData,
		CancellationToken cancellationToken)
	{
		ValidationResult validation = await ValidateAsync(userData, cancellationToken);

		if (validation.IsValid == false)
			return BadRequest(validation.Errors);

		await _userRepository.Register(userData, cancellationToken);

		return Ok();
	}

	[HttpPut]
	public async Task<IActionResult> UpdateUser(long id, CancellationToken cancellationToken)
	{
		await _userRepository.UpdateUser(id, cancellationToken);
		return Ok();
	}

	private async Task<ValidationResult> ValidateAsync(UserDataTransferObject userData, CancellationToken cancellationToken) =>
		await _userRegistrationValidator.ValidateAsync(userData, cancellationToken);

	private async Task<ValidationResult> ValidateAsync(long id, CancellationToken cancellationToken) =>
		await _userValidator.ValidateAsync(id, cancellationToken);
}
