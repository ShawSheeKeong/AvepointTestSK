using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] string cafe = null)
    {
        IQueryable<Employee> query = _context.Employees
            .Where(e => !e.IsDeleted)
            .Include(e => e.CafeEmployees)
            .ThenInclude(ce => ce.Cafe);

        if (!string.IsNullOrEmpty(cafe))
        {
            query = query.Where(e => e.CafeEmployees
                .Any(ce => ce.Cafe.Name.Equals(cafe, StringComparison.OrdinalIgnoreCase)));
        }

        var employees = await query.ToListAsync();

        var result = employees.Select(e => new
        {
            e.Id,
            e.Name,
            e.EmailAddress,
            e.PhoneNumber,
            DaysWorked = e.CafeEmployees
                    .OrderByDescending(ce => ce.StartDate)
                    .Select(ce => (int)(DateTime.UtcNow - ce.StartDate).TotalDays)
                    .FirstOrDefault(),
            Cafe = e.CafeEmployees
                    .OrderByDescending(ce => ce.StartDate)
                    .Select(ce => ce.Cafe.Name)
                    .FirstOrDefault()
        })
            .OrderByDescending(e => e.DaysWorked)
            .ToList();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.EmailAddress))
        {
            return BadRequest("Invalid request data.");
        }

        var cafe = await _context.Cafes.FindAsync(request.CafeId);
        if (cafe == null || cafe.IsDeleted)
        {
            return NotFound($"Café with ID {request.CafeId} not found.");
        }

        var employee = new Employee
        {
            Id = GenerateEmployeeId(),
            Name = request.Name,
            EmailAddress = request.EmailAddress,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            CreatedBy = request.CreatedBy,
            CreatedTime = DateTime.UtcNow,
            LastUpdatedBy = request.CreatedBy,
            LastUpdatedTime = DateTime.UtcNow,
        };

        await _context.Employees.AddAsync(employee);

        var cafeEmployee = new CafeEmployee
        {
            CafeId = request.CafeId,
            EmployeeId = employee.Id,
            StartDate = DateTime.UtcNow,
            CreatedBy = request.CreatedBy,
            CreatedTime = DateTime.UtcNow,
            LastUpdatedBy = request.CreatedBy,
            LastUpdatedTime = DateTime.UtcNow,
            IsDeleted = false
        };

        await _context.CafeEmployees.AddAsync(cafeEmployee);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateEmployee), new { id = employee.Id }, new
        {
            Employee = employee,
            Relationship = cafeEmployee
        });
    }


    [HttpPut("/employee/{id}")]
    public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeRequest request)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null || employee.IsDeleted)
            return NotFound("Employee not found.");

        var cafe = await _context.Cafes.FindAsync(request.CafeId);
        if (request.CafeId != null && (cafe == null || cafe.IsDeleted))
            return NotFound("Café not found.");

        employee.Name = request.Name ?? employee.Name;
        employee.EmailAddress = request.EmailAddress ?? employee.EmailAddress;
        employee.PhoneNumber = request.PhoneNumber ?? employee.PhoneNumber;
        employee.LastUpdatedBy = request.LastUpdatedBy;
        employee.LastUpdatedTime = DateTime.UtcNow;

        if (request.CafeId != null)
        {
            var cafeEmployee = await _context.CafeEmployees
                .FirstOrDefaultAsync(ce => ce.EmployeeId == id);

            if (cafeEmployee != null)
                cafeEmployee.CafeId = request.CafeId.Value;
            else
            {
                cafeEmployee = new CafeEmployee
                {
                    CafeId = request.CafeId.Value,
                    EmployeeId = id,
                    StartDate = DateTime.UtcNow,
                    CreatedBy = request.LastUpdatedBy,
                    CreatedTime = DateTime.UtcNow,
                    LastUpdatedBy = request.LastUpdatedBy,
                    LastUpdatedTime = DateTime.UtcNow,
                    IsDeleted = false
                };
                await _context.CafeEmployees.AddAsync(cafeEmployee);
            }
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null || employee.IsDeleted)
            return NotFound();

        employee.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private string GenerateEmployeeId()
    {
        string prefix = "UI";
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        string uniquePart = new string(
            new char[7]
            .Select(_ => chars[random.Next(chars.Length)])
            .ToArray());

        return prefix + uniquePart;
    }
}
