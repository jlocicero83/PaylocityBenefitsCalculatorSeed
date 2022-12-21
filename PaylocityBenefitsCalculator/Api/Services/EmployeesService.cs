using Api.DataAccess;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services
{
    public class EmployeesService
    {
        private BenefitsCalcDbContext _dbContext;

        //Inject db context through constructor
        public EmployeesService(BenefitsCalcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<GetEmployeeDto> GetAllEmployees()
        {
            //Future Flex/Performance -- add server side pagination to handle increasingly large resultsets
            List<GetEmployeeDto> employeeDtos = new List<GetEmployeeDto>();
            
                var employees = _dbContext.Employee;



            //TODO: Refactor/extract this
            foreach (Employee employee in employees)
            {
                employeeDtos.Add(new GetEmployeeDto()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,

                });
            }
            return employeeDtos;
        }

         
    }
}
