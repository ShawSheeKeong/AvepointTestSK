using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CafeController : ControllerBase
{
    private readonly AppDbContext _context;

    public CafeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCafes([FromQuery] string location = null)
    {
        IQueryable<Cafe> query = _context.Cafes
                        .Where(c => !c.IsDeleted)
                        .Include(c => c.CafeEmployees)
                        .ThenInclude(ce => ce.Employee);

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(c => c.CafeEmployees
                .Any(ce => ce.Cafe.Location.Equals(location, StringComparison.OrdinalIgnoreCase)));
        }

        var cafes = await query.Select(c => new
        {
            c.Id,
            c.Name,
            c.Description,
            Employees = c.CafeEmployees.Count,
            c.Logo,
            c.Location
        })
        .OrderByDescending(c => c.Employees)
        .ToListAsync();

        return Ok(cafes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCafe([FromBody] Cafe cafe)
    {
        if (cafe == null)
            return BadRequest();

        _context.Cafes.Add(cafe);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateCafe), new { id = cafe.Id }, cafe);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCafe(Guid id, [FromBody] Cafe updatedCafe)
    {
        var cafe = await _context.Cafes.FindAsync(id);
        if (cafe == null || cafe.IsDeleted)
            return NotFound();

        cafe.Name = updatedCafe.Name;
        cafe.Description = updatedCafe.Description;
        cafe.Logo = updatedCafe.Logo;
        cafe.Location = updatedCafe.Location;
        cafe.LastUpdatedTime = DateTime.UtcNow;
        cafe.LastUpdatedBy = updatedCafe.LastUpdatedBy;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("/cafe/{id}")]
    public async Task<IActionResult> DeleteCafe(Guid id)
    {
        var cafe = await _context.Cafes
            .Include(c => c.CafeEmployees)
            .ThenInclude(ce => ce.Employee)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cafe == null || cafe.IsDeleted)
        {
            return NotFound("Café not found.");
        }

        cafe.IsDeleted = true;

        foreach (var cafeEmployee in cafe.CafeEmployees)
        {
            cafeEmployee.IsDeleted = true;

            if (cafeEmployee.Employee != null)
            {
                cafeEmployee.Employee.IsDeleted = true;
            }
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

}
