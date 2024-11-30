using MediatR;

public class CreateEmployeeCommand : IRequest<string>
{
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public string CreatedBy { get; set; }
    public Guid CafeId { get; set; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
{
    private readonly AppDbContext _context;

    public CreateEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.EmailAddress))
        {
            throw new KeyNotFoundException("Invalid request data.");
        }

        var cafe = await _context.Cafes.FindAsync(request.CafeId);
        if (cafe == null || cafe.IsDeleted)
        {
            throw new KeyNotFoundException($"Café ID {request.CafeId} not found.");
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

        await _context.Employees.AddAsync(employee, cancellationToken);

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

        await _context.CafeEmployees.AddAsync(cafeEmployee, cancellationToken);

        await _context.SaveChangesAsync();

        return employee.Id;
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
