using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(CreateCafeCommand).Assembly);
builder.Services.AddMediatR(typeof(GetAllCafeCommand).Assembly);
builder.Services.AddMediatR(typeof(UpdateCafeCommand).Assembly);
builder.Services.AddMediatR(typeof(DeleteCafeCommand).Assembly);

var app = builder.Build();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
