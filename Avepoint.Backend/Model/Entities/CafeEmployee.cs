public class CafeEmployee
{
    public int Id { get; set; }
    public Guid CafeId { get; set; }
    public string EmployeeId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
    public DateTime LastUpdatedTime { get; set; } = DateTime.UtcNow;
    public string LastUpdatedBy { get; set; }

    public Cafe Cafe { get; set; }
    public Employee Employee { get; set; }
}
