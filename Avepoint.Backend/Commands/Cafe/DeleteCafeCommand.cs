using MediatR;
using Microsoft.EntityFrameworkCore;

public class DeleteCafeCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand>
{
    private readonly AppDbContext _context;

    public DeleteCafeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _context.Cafes
            .Include(c => c.CafeEmployees)
            .ThenInclude(ce => ce.Employee)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (cafe == null || cafe.IsDeleted)
        {
            throw new KeyNotFoundException($"Cafe with ID {request.Id} not found.");
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

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}