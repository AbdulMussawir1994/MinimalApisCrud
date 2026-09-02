using Microsoft.EntityFrameworkCore;
using MinimalApisCrud.DataDbContext;
using MinimalApisCrud.Entity.DTOs;
using MinimalApisCrud.Interface;
using MinimalApisCrud.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        SqlOptions =>
        {
            SqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(1).TotalSeconds);
        });
});
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
//MinimalApis

app.MapGet("/List", async (IEmployeeService service) =>
Results.Ok(await service.GetEmployeesAsync()));

app.MapPost("/Add", async (EmployeeDto model, IEmployeeService service) =>
{
    var isCreated = await service.AddEmployeeAsync(model);
    return Results.Created($"/Add", isCreated);
});

app.MapPut("/Update", async (EmployeeDto model, IEmployeeService service) =>
{
    var isUpdated = await service.UpdateEmployeeAsync(model);
    return Results.Ok(isUpdated);
});

app.MapGet("/GetById/{id:int}", async (int id, IEmployeeService service) =>
Results.Ok(await service.GetEmployeeByIdAsync(id)));

app.MapDelete("/Delete/{id:int}", async (int id, IEmployeeService service) =>
Results.Ok(await service.DeleteEmployeeByIdAsync(id)));

app.Run();
