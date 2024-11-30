using MediatR;

public class CreateCafeCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
    public string Location { get; set; }
    public string CreatedBy { get; set; }
    public string LastUpdatedBy { get; set; }
}

public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
{
    private readonly AppDbContext _context;

    public CreateCafeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = new Cafe
        {
            Name = request.Name,
            Description = request.Description,
            Logo = request.Logo,
            Location = request.Location,
            CreatedBy = request.CreatedBy,
            LastUpdatedBy = request.LastUpdatedBy
        };

        _context.Cafes.Add(cafe);
        await _context.SaveChangesAsync(cancellationToken);

        return cafe.Id;
    }
}
