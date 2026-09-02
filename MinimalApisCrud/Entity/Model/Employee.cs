using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApisCrud.Entity.Model;

[Table("Employee", Schema = "dbo")]
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}
