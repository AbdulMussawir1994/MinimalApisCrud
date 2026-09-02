using MinimalApisCrud.Entity.DTOs;
using MinimalApisCrud.Helper;

namespace MinimalApisCrud.Interface;

public interface IEmployeeService
{
    Task<GenericResponse<IReadOnlyList<EmployeeDto>>> GetEmployeesAsync();
    Task<GenericResponse<int>> AddEmployeeAsync(EmployeeDto model);
    Task<GenericResponse<EmployeeDto>> UpdateEmployeeAsync(EmployeeDto model);
    Task<GenericResponse<EmployeeDto>> GetEmployeeByIdAsync(int id);
    Task<GenericResponse<bool>> DeleteEmployeeByIdAsync(int id);
}
