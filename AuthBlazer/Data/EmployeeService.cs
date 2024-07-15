
using AuthBlazer.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthBlazer.Data
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EmployeeService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<Employee>> SearchEmployees(string searchString)
        {
            // Perform a search query based on your requirements
            return await _applicationDbContext.Employees
                .Where(e => e.FullName.Contains(searchString)) // Adjust this according to your Employee model
                .ToListAsync();
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _applicationDbContext.Employees
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Address = e.Address,
                    Email = e.Email,
                    Phone = e.Phone,
                    City = e.City,
                    Designation = e.Designation,
                    ImagePath = e.ImagePath ?? new byte[0]  // Provide a default value or handle nulls here
                })
                .ToListAsync();
        }

        public async Task<bool> AddNewEmployee(Employee employee)
        {
            await _applicationDbContext.Employees.AddAsync(employee);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        }
        public async Task<bool> UpdateEmployeeDetails(Employee employee)
        {
            _applicationDbContext.Employees.Update(employee);
            await _applicationDbContext.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteEmployee(Employee employee)
        {
            _applicationDbContext.Employees.Remove(employee);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<EmployeeCountByCity>> GetEmployeeCountByCity()
        {
            var result = await _applicationDbContext.Employees
                .GroupBy(e => e.City)
                .Select(g => new EmployeeCountByCity
                {
                    City = g.Key,
                    EmployeeCount = g.Count()
                })
                .ToListAsync();

            return result;
        }
        public async Task<int> GetTotalEmployeeCount()
        {
            return await _applicationDbContext.Employees.CountAsync();
        }
        public async Task<List<EmployeeCountByDesignation>> GetEmployeeCountByDesignation()
        {
            var result = await _applicationDbContext.Employees
                .GroupBy(e => e.Designation)
                .Select(g => new EmployeeCountByDesignation
                {
                    Designation = g.Key,
                    EmployeeCount = g.Count()
                })
                .ToListAsync();

            return result;
        }
        public async Task<int> GetTotalDistinctDesignations()
        {
            var count = await _applicationDbContext.Employees
                .Select(e => e.Designation)
                .Distinct()
                .CountAsync();

            return count;
        }

    }

    public class EmployeeCountByCity
    {
        public string City { get; set; }
        public int EmployeeCount { get; set; }
    }
    public class EmployeeCountByDesignation
    {
        public string Designation { get; set; }
        public int EmployeeCount { get; set; }
    }
}