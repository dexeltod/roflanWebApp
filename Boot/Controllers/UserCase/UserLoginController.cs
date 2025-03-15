using Application;
using Application.DTO;
using Application.Factories;
using Application.Validation;
using FluentValidation.Results;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace Boot.Controllers.UserCase;

public class UserLoginController : ControllerBase
{
	private readonly IConfiguration _configuration;

	private readonly IJwtTokenFactory _jwtTokenFactory;
	private readonly ILogger<UserRegistrationController> _logger;
	private readonly IUserLoginValidator _userRegistrationValidator;

	public UserLoginController(
		ILogger<UserRegistrationController> logger,
		IUserLoginValidator userRegistrationValidator,
		IJwtTokenFactory jwtTokenFactory,
		IConfiguration configuration
	)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_userRegistrationValidator =
			userRegistrationValidator ?? throw new ArgumentNullException(nameof(userRegistrationValidator));
		_jwtTokenFactory = jwtTokenFactory ?? throw new ArgumentNullException(nameof(jwtTokenFactory));
		_configuration = configuration;
	}

	[HttpPost("login-user")]
	public async Task<IActionResult> Login(
		[FromBody] UserDataTransferObject userData,
		CancellationToken cancellationToken,
		HttpClient httpClient
	)
	{
		string? uri = _configuration["Services:Login"] ?? throw new ArgumentNullException(_configuration["Services:Login"]);


		GrpcChannel channel = GrpcChannel.ForAddress(uri, new GrpcChannelOptions { HttpClient = httpClient });
		var client = new Greeter.GreeterClient(channel);

		var request = new HelloRequest { Name = "pahan" };

		HelloReply reply = await client.SayHelloAsync(request);

		_logger.LogInformation("Received reply: {Message}", reply.Message);

		return Ok(reply.Message);


	}

	private async Task<ValidationResult> ValidateAsync(UserDataTransferObject userData, CancellationToken cancellationToken) =>
		await _userRegistrationValidator.ValidateAsync(userData, cancellationToken);
}
