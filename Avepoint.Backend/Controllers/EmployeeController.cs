using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employee = await _mediator.Send(new GetAllEmployeeCommand());
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        if (command == null) return BadRequest();

        var employeeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateEmployee), new { id = employeeId });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeCommand command)
    {
        if (command == null) return BadRequest();
        command.Id = id;

        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        await _mediator.Send(new DeleteEmployeeCommand { Id = id });
        return NoContent();
    }
}
