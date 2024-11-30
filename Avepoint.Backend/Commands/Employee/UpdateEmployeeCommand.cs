using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateEmployeeCommand : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public Guid? CafeId { get; set; }
    public string LastUpdatedBy { get; set; }
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly AppDbContext _context;

    public UpdateEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(request.Id);
        if (employee == null || employee.IsDeleted)
            throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");

        var cafe = await _context.Cafes.FindAsync(request.CafeId);
        if (request.CafeId != null && (cafe == null || cafe.IsDeleted))
            throw new KeyNotFoundException($"Cafe not found.");

        employee.Name = request.Name ?? employee.Name;
        employee.EmailAddress = request.EmailAddress ?? employee.EmailAddress;
        employee.PhoneNumber = request.PhoneNumber ?? employee.PhoneNumber;
        employee.Gender = request.Gender ?? employee.Gender;
        employee.LastUpdatedBy = request.LastUpdatedBy;
        employee.LastUpdatedTime = DateTime.UtcNow;

        if (request.CafeId != null)
        {
            var cafeEmployee = await _context.CafeEmployees
                .FirstOrDefaultAsync(ce => ce.EmployeeId == request.Id);

            if (cafeEmployee != null)
                cafeEmployee.CafeId = request.CafeId.Value;
            else
            {
                cafeEmployee = new CafeEmployee
                {
                    CafeId = request.CafeId.Value,
                    EmployeeId = request.Id,
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

        return Unit.Value;
    }
}