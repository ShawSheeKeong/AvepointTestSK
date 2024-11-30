using MediatR;

public class UpdateCafeCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
    public string Location { get; set; }
    public string LastUpdatedBy { get; set; }
}

public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand>
{
    private readonly AppDbContext _context;

    public UpdateCafeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _context.Cafes.FindAsync(request.Id);
        if (cafe == null || cafe.IsDeleted)
            throw new KeyNotFoundException($"Cafe with ID {request.Id} not found.");

        cafe.Name = request.Name;
        cafe.Description = request.Description;
        cafe.Logo = request.Logo;
        cafe.Location = request.Location;
        cafe.LastUpdatedTime = DateTime.UtcNow;
        cafe.LastUpdatedBy = request.LastUpdatedBy;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}