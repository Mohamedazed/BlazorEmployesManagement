using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthBlazer.Data
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }
        public byte[]? ImagePath { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
