using Application.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Utils.ConfigurationModels;

namespace Boot.Controllers;

public class TestController : ControllerBase

{
	private readonly AuthorizationOptions _authorizationOptions;
	private readonly RabbitMqService _mqService;

	public TestController(
		IOptions<AuthorizationOptions> authorizationOptions,
		RabbitMqService mqService
	)
	{
		_mqService = mqService ?? throw new ArgumentNullException(nameof(mqService));
		_authorizationOptions = authorizationOptions.Value;
	}

	// [HttpGet("rabbitGet")]
	// public async Task<IActionResult> RabbitMqReceive()
	// {
	// 	await _mqService.Receive();
	//
	// 	return Ok("RabbitMQ test received");
	// }

	[HttpGet("rabbitPost")]
	public async Task<IActionResult> RabbitMqSend(string payload = "HelloWorld")
	{
		await _mqService.SendMessage(payload);
		return Ok("RabbitMQ test passed");
	}

	[HttpGet("test")]
	public Task<IActionResult> Test() => Task.FromResult<IActionResult>(Ok("test"));
}
