using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace Boot.Controllers.ToServices;

[Route("api/[controller]")]
public class AuthServiceController : ControllerBase
{
	[HttpPost("add-user")]
	public async void Add()
	{
		using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5198");
	}
}
