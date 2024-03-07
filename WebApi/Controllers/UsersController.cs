using Application.Features.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] CreateUserCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}