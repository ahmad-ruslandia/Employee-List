using System.ComponentModel.DataAnnotations;

namespace EmployeeProfile.Models
{
    public class Employee
    {
        [Key]
        public int IdEmployee{ get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime ContractDate { get; set; }
    }
}
