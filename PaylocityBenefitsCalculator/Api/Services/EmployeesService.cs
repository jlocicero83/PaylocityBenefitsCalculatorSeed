using Api.DataAccess;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Api.Utilities;
using Api.Dtos.Paycheck;

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
         //TODO: DRY - extract this later----------------------------------------------------------------
            var employee = await _dbContext.Employee.SingleOrDefaultAsync(emp => emp.Id == id);
            if (employee is null) return null;
            else
            {
                employee.Dependents = await GetDependentsOfEmployee(employee.Id);
         //----------------------------------------------------------------------------------------------
                return DtoMapper.MapEmployeeToGetDto(employee);
            }
        }

        public async Task<GetEmployeesPaycheckDto?> GetEmployeesPaycheck(int id)
        {

         //TODO: DRY - extract this later----------------------------------------------------------------
            var employee = await _dbContext.Employee.SingleOrDefaultAsync(emp => emp.Id == id);
            if (employee is null) return null;
            else
            {
                employee.Dependents = await GetDependentsOfEmployee(employee.Id);
         //----------------------------------------------------------------------------------------------
                Paycheck paycheck = new Paycheck(employee);
                return DtoMapper.MapPaycheckToDto(paycheck);
            }
        }

        private async Task<List<Dependent>> GetDependentsOfEmployee(int relatedEmployeeId)
        {
            return await _dbContext.Dependent
                .Where(dep => dep.EmployeeId == relatedEmployeeId)
                .ToListAsync();
        }


        #endregion

        #region PUT Methods

        public async Task<GetEmployeeDto?> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            var existing = await _dbContext.Employee.SingleOrDefaultAsync(emp => emp.Id == id);
            if(existing is null || updatedEmployee is null) return null;

            else
            {
                //update existing record and save changes
                existing.FirstName = updatedEmployee.FirstName;
                existing.LastName = updatedEmployee.LastName;
                existing.Salary = updatedEmployee.Salary;
                _dbContext.Update(existing);
                int recordsUpdated = await _dbContext.SaveChangesAsync();
                return recordsUpdated == 1 ? DtoMapper.MapEmployeeToGetDto(existing) : null;
            }
        }

        #endregion

        #region POST Methods
        public async Task<AddEmployeeDto?> AddNewEmployee(AddEmployeeDto addEmployeeDto)
        {

            if (addEmployeeDto is null) return null;

            //check if identical record exists
            Employee employee = DtoMapper.MapDtoToEmployee(addEmployeeDto);
            var existed = _dbContext.Employee
                                    .Where(emp => emp.LastName == employee.LastName
                                            && emp.DateOfBirth.Equals(employee.DateOfBirth))
                                    .FirstOrDefault();
            if (existed is not null) return addEmployeeDto;
            
            //if no existing record, add the employee to Employee table
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

        #region DELETE Methods
        public async Task<GetEmployeeDto?> DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employee.FindAsync(id);
            if (employee is null) return null;

            //first remove any dependent records associated with that employee
            List<Dependent> dependents = await _dbContext.Dependent.Where(dep => dep.EmployeeId == id).ToListAsync();
            _dbContext.Dependent.RemoveRange(dependents);
            _dbContext.SaveChanges();

            //then delete the employee record
            _dbContext.Employee.Remove(employee);
            int recordsRemoved = _dbContext.SaveChanges();
            return recordsRemoved == 0 ? null : DtoMapper.MapEmployeeToGetDto(employee);

        }

        #endregion
    }
}
