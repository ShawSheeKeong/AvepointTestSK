using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<CafeEmployee> CafeEmployees { get; set; }

    public DbSet<Cafe> Cafes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("T_Employee");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.EmailAddress)
                .HasColumnName("email_address")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedTime)
                .HasColumnName("created_time")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.LastUpdatedTime)
                .HasColumnName("last_updated_time")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(e => e.LastUpdatedBy)
                .HasColumnName("last_updated_by")
                .HasMaxLength(200)
                .IsRequired();
        });

        modelBuilder.Entity<CafeEmployee>(entity =>
        {
            entity.ToTable("T_Cafe_Employee");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.CafeId)
                .HasColumnName("cafe_id")
                .IsRequired();

            entity.Property(e => e.EmployeeId)
                .HasColumnName("employee_id")
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(e => e.StartDate)
                .HasColumnName("start_date")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(e => e.EndDate)
                .HasColumnName("end_date")
                .IsRequired(false);

            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedTime)
                .HasColumnName("created_time")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.LastUpdatedTime)
                .HasColumnName("last_updated_time")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(e => e.LastUpdatedBy)
                .HasColumnName("last_updated_by")
                .HasMaxLength(200)
                .IsRequired();

            entity.HasOne(e => e.Cafe)
                .WithMany(c => c.CafeEmployees)
                .HasForeignKey(e => e.CafeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Employee)
                  .WithMany(e => e.CafeEmployees)
                  .HasForeignKey(e => e.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);


            entity.HasIndex(e => new { e.CafeId, e.EmployeeId }).IsUnique();
        });

        modelBuilder.Entity<Cafe>(entity =>
        {
            entity.ToTable("T_Cafe");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(e => e.Logo)
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedTime)
                .HasColumnName("created_time")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.LastUpdatedTime)
                .HasColumnName("last_updated_time")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entity.Property(e => e.LastUpdatedBy)
                .HasColumnName("last_updated_by")
                .HasMaxLength(200)
                .IsRequired();

        });
    }
}
