using Microsoft.EntityFrameworkCore;
using MinimalApisCrud.DataDbContext;
using MinimalApisCrud.Entity.DTOs;
using MinimalApisCrud.Entity.Model;
using MinimalApisCrud.Helper;
using MinimalApisCrud.Interface;

namespace MinimalApisCrud.Repository;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeDbContext _context;

    public EmployeeService(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<int>> AddEmployeeAsync(EmployeeDto model)
    {
        var emp = new Employee
        {
            Name = model.name,
            Department = model.department,
            Salary = model.salary,
        };

        _context.Add(emp);
        var result = await _context.SaveChangesAsync() > 0;

        return result ? GenericResponse<int>.Success(emp.Id, true, "Employee add successfully", 201)
                                : GenericResponse<int>.Failure(false, "Add employee failed", 400);
    }

    public async Task<GenericResponse<bool>> DeleteEmployeeByIdAsync(int id)
    {
        var emp = await _context.Employees.FindAsync(id);

        if (emp is null)
        {
            return GenericResponse<bool>.Failure(false, " Invalid employee id.", 204);
        }

        _context.Remove(emp);//
        await _context.SaveChangesAsync();//
        return GenericResponse<bool>.Success(true, true, "Employee deleted successfully.", 200);
    }

    public async Task<GenericResponse<EmployeeDto>> GetEmployeeByIdAsync(int id)
    {
        var emp = await _context.Employees.AsNoTracking().Where(x => x.Id == id)
            .Select(x => new EmployeeDto(x.Id, x.Name, x.Department, x.Salary)).FirstOrDefaultAsync();


        if (emp is null)
        {
            return GenericResponse<EmployeeDto>.Failure(false, " Invalid employee id.", 204);
        }

        return GenericResponse<EmployeeDto>.Success(emp, true, "Employee fetched successfully.", 200);
    }

    public async Task<GenericResponse<IReadOnlyList<EmployeeDto>>> GetEmployeesAsync()
    {
        var emp = await _context.Employees
            .AsNoTracking()
            .Select(x => new EmployeeDto(x.Id, x.Name, x.Department, x.Salary))
            .ToListAsync();

        return emp.Any() ? GenericResponse<IReadOnlyList<EmployeeDto>>.Success(emp, true, "Employee fetched successfully", 200)
                                         : GenericResponse<IReadOnlyList<EmployeeDto>>.Empty(Array.Empty<EmployeeDto>(), true, "Employee fetched successfully", 200);
    }

    public async Task<GenericResponse<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto model)
    {
        var emp = await _context.Employees.FindAsync(model.id);

        if (emp is null)
        {
            return GenericResponse<EmployeeDto>.Failure(false, " Invalid employee id.", 204);
        }

        emp.Name = model.name;
        emp.Department = model.department;
        emp.Salary = model.salary;

        _context.Update(emp);
        await _context.SaveChangesAsync();
        return GenericResponse<EmployeeDto>.Success(model, true, "Employee updated successfully.", 200);
    }
}
