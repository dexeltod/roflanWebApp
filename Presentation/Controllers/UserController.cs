using Application.DTO;
using Domain.Models;
using FluentValidation.Results;
using Infrastructure.Repositories;
using Infrastructure.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class UserController(ILogger<UserController> logger, UserRepository userRepository, UserValidator userValidator)
    : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly UserRepository _userRepository =
        userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    private readonly UserValidator _userValidator =
        userValidator ?? throw new ArgumentNullException(nameof(userValidator));

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDataTransferObject userData)
    {
        var validation = await ValidateAsync(userData);

        if (validation.IsValid == false)
            return BadRequest();

        await _userRepository.Register(userData.Name, userData.Age, userData.Password, userData.Email);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(int[] ids)
    {
        var users = await _userRepository.GetUsers(ids);

        return Ok(users);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        User? user = await _userRepository.GetUserById(id);

        if (user == null)
            return BadRequest();

        return Ok(user);
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> Create([FromBody] UserDataTransferObject userData)
    {
        await _userRepository.AddUser(userData);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userRepository.DeleteUser(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(int id)
    {
        await _userRepository.UpdateUser(id);
        return Ok();
    }

    private async Task<ValidationResult> ValidateAsync(UserDataTransferObject userData)
    {
        return await _userValidator.ValidateAsync(userData);
    }
}