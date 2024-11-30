using MediatR;

public class DeleteEmployeeCommand : IRequest
{
    public string Id { get; set; }
}

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly AppDbContext _context;

    public DeleteEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(request.Id, cancellationToken);
        if (employee == null || employee.IsDeleted)
        {
            throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
        }

        employee.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}