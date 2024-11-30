using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAllCafeCommand : IRequest<List<CafeDTO>>
{
    public string? Location { get; set; }
}

public class GetAllCafeCommandHandler : IRequestHandler<GetAllCafeCommand, List<CafeDTO>>
{
    private readonly AppDbContext _context;

    public GetAllCafeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CafeDTO>> Handle(GetAllCafeCommand request, CancellationToken cancellationToken)
    {
        IQueryable<Cafe> query = _context.Cafes
                        .Where(c => !c.IsDeleted
                        || (!string.IsNullOrEmpty(request.Location)
                            && c.Location.ToLower().Contains(request.Location.ToLower())))
                        .Include(c => c.CafeEmployees)
                        .ThenInclude(ce => ce.Employee);

        var cafes = await query.Select(c => new CafeDTO()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description ?? string.Empty,
            Employees = c.CafeEmployees.Count,
            Logo = c.Logo ?? string.Empty,
            Location = c.Location
        })
        .OrderByDescending(c => c.Employees)
        .ToListAsync();
        return cafes;
    }
}
