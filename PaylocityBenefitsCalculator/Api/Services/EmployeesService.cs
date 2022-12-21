using Api.DataAccess;
using Api.Dtos.Employee;
using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Services
{
    public class EmployeesService
    {
        private BenefitsCalcDbContext _dbContext;
        private DtoMappingService _mapper;

        //Inject dbContext through constructor
        public EmployeesService(BenefitsCalcDbContext dbContext, DtoMappingService mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployees()
        {
            //Performance opt. -- could add server side pagination to handle increasingly large resultsets. For now just taking the first 25
            List<GetEmployeeDto> results = new List<GetEmployeeDto>();

            var employees = _dbContext.Employee.Take(25).ToList();
            foreach(Employee employee in employees)
            {
                employee.Dependents = GetDependentsOfEmployee(employee.Id);
                results.Add(_mapper.MapEmployeeToDto(employee));
            }
            return results;
        }


        private List<Dependent> GetDependentsOfEmployee(int relatedEmployeeId)
        {
            return _dbContext.Dependent
                .Where(dep => dep.EmployeeId == relatedEmployeeId)
                .ToList();
        }

         
    }
}
