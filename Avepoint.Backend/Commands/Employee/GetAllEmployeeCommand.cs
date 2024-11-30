using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllEmployeeCommand : IRequest<List<EmployeeDTO>>
{
}

public class GetAllEmployeeCommandHandler : IRequestHandler<GetAllEmployeeCommand, List<EmployeeDTO>>
{
    private readonly AppDbContext _context;

    public GetAllEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDTO>> Handle(GetAllEmployeeCommand request, CancellationToken cancellationToken)
    {
        IQueryable<Employee> query = _context.Employees
            .Where(e => !e.IsDeleted)
            .Include(e => e.CafeEmployees)
            .ThenInclude(ce => ce.Cafe);

        var result = await query.Select(e => new EmployeeDTO()
        {
            Id = e.Id,
            Name = e.Name,
            EmailAddress = e.EmailAddress,
            PhoneNumber = e.PhoneNumber,
            Gender = e.Gender,
            DaysWorked = e.CafeEmployees
                    .OrderByDescending(ce => ce.StartDate)
                    .Select(ce => (int)(DateTime.UtcNow - ce.StartDate).TotalDays)
                    .FirstOrDefault(),
            CafeId = e.CafeEmployees
                    .OrderByDescending(ce => ce.StartDate)
                    .Select(ce => ce.Cafe.Id)
                    .FirstOrDefault(),
            CafeName = e.CafeEmployees
                    .OrderByDescending(ce => ce.StartDate)
                    .Select(ce => ce.Cafe.Name)
                    .FirstOrDefault()
        }).ToListAsync();
        return result.OrderByDescending(e => e.DaysWorked).ToList();







    }
}
