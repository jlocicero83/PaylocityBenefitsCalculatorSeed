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
                employee.Dependents = await GetDependentsOfEmployee(employee.Id);
                results.Add(DtoMapper.MapEmployeeToDto(employee));
            }
            return results;
        }


        private async Task<List<Dependent>> GetDependentsOfEmployee(int relatedEmployeeId)
        {
            return await _dbContext.Dependent
                .Where(dep => dep.EmployeeId == relatedEmployeeId)
                .ToListAsync();
        }

         
    }
}
