using Api.DataAccess;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Api.Utilities;

namespace Api.Services
{
    public class EmployeesService
    {
        private BenefitsCalcDbContext _dbContext;
        
        public EmployeesService(BenefitsCalcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployees()
        {
            //Performance opt. -- could add server side pagination to handle increasingly large resultsets. 
            List<GetEmployeeDto> results = new List<GetEmployeeDto>();

            var employees = await _dbContext.Employee.ToListAsync();
            foreach(Employee employee in employees)
            {
                //This could maybe be optimized - perhaps only showing dependents on an employee-detail screen of sorts? Could avoid the expensive op.
                //of getting every dependent for every employee. Instead get the dependents for a specific employee on a need-to basis.              
                employee.Dependents = await GetDependentsOfEmployee(employee.Id);
                results.Add(DtoMapper.MapEmployeeToDto(employee));
            }
            return results;
        }

        public async Task<GetEmployeeDto?> GetEmployeeById(int id)
        {
            var employee = await _dbContext.Employee.SingleOrDefaultAsync(emp => emp.Id == id);
            if (employee is null) return null;
            else
            {
                employee.Dependents = await GetDependentsOfEmployee(employee.Id);
                return DtoMapper.MapEmployeeToDto(employee);
            }
        }


        private async Task<List<Dependent>> GetDependentsOfEmployee(int relatedEmployeeId)
        {
            return await _dbContext.Dependent
                .Where(dep => dep.EmployeeId == relatedEmployeeId)
                .ToListAsync();
        }

         
    }
}
