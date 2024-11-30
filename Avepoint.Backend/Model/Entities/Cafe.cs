public class Cafe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public string Location { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
    public DateTime LastUpdatedTime { get; set; } = DateTime.UtcNow;
    public string LastUpdatedBy { get; set; }

    public ICollection<CafeEmployee> CafeEmployees { get; set; } = new List<CafeEmployee>();
}

public class CafeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
    public string Location { get; set; }
    public int Employees { get; set; }

}