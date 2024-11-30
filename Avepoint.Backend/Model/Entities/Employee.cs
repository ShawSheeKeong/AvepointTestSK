public class Employee
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
    public DateTime LastUpdatedTime { get; set; } = DateTime.UtcNow;
    public string LastUpdatedBy { get; set; }

    public ICollection<CafeEmployee> CafeEmployees { get; set; } = new List<CafeEmployee>();
}

public class EmployeeDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public int DaysWorked { get; set; }
    public Guid CafeId { get; set; }
    public string? CafeName { get; set; }
}