using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services
{
    public class DtoMappingService
    {
        public GetDependentDto MapDependentToDto(Dependent dependent)
        {
            return new GetDependentDto()
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                DateOfBirth = dependent.DateOfBirth,
                Relationship = dependent.Relationship
            };
        }

        public GetEmployeeDto MapEmployeeToDto(Employee employee)
        {
            GetEmployeeDto employeeDto = new GetEmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth
            };
            foreach(Dependent dependent in employee.Dependents)
            {
                employeeDto.Dependents.Add(MapDependentToDto(dependent));
            }
            return employeeDto;
     
        }
    }
}
