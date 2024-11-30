using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CafeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CafeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCafes([FromQuery] string location = null)
    {
        var cafe = await _mediator.Send(new GetAllCafeCommand { Location = location });
        return Ok(cafe);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCafe([FromBody] CreateCafeCommand command)
    {
        if (command == null) return BadRequest();

        var cafeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateCafe), new { id = cafeId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCafe(Guid id, [FromBody] UpdateCafeCommand command)
    {
        if (command == null) return BadRequest();
        command.Id = id;

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCafe(Guid id)
    {
        await _mediator.Send(new DeleteCafeCommand { Id = id });
        return NoContent();
    }
}
