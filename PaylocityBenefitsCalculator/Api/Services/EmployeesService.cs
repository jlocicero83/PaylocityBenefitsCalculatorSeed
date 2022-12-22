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

        #region GET Methods

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
                results.Add(DtoMapper.MapEmployeeToGetDto(employee));
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
                return DtoMapper.MapEmployeeToGetDto(employee);
            }
        }

       


        private async Task<List<Dependent>> GetDependentsOfEmployee(int relatedEmployeeId)
        {
            return await _dbContext.Dependent
                .Where(dep => dep.EmployeeId == relatedEmployeeId)
                .ToListAsync();
        }

        #endregion

        #region POST Methods
        public async Task<AddEmployeeDto?> AddNewEmployee(AddEmployeeDto addEmployeeDto)
        {
            //first, add the employee to Employee table
            Employee employee = new Employee()
            {
                FirstName = addEmployeeDto.FirstName,
                LastName = addEmployeeDto.LastName,
                Salary = addEmployeeDto.Salary,
                DateOfBirth = addEmployeeDto.DateOfBirth
            };
            await _dbContext.Employee.AddAsync(employee);
            int recordsAdded = _dbContext.SaveChanges();


            if (recordsAdded == 0) return null;
            
            //check if there are any dependents to add
            else if (addEmployeeDto.Dependents is null || addEmployeeDto.Dependents.Count == 0) return DtoMapper.MapEmployeeToAddDto(employee);
            else
            {
                //EXTRACT THIS - add the dependents to the db
                List<Dependent> dependents = new List<Dependent>();
                foreach(var dep in addEmployeeDto.Dependents)
                {
                    dep.EmployeeId = employee.Id;
                    dependents.Add(DtoMapper.MapDtoToDependent(dep));
                }
                await _dbContext.AddRangeAsync(dependents);
                _dbContext.SaveChanges();
            }
            return DtoMapper.MapEmployeeToAddDto(employee);
        }

        #endregion
    }
}
