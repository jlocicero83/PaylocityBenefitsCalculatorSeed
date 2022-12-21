using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services
{
    public class DtoMappingService
    {
        /*I'm not too familiar with the DTO pattern - I added this service to handle all the mapping needs for models -> dtos. Sending full models to the
         * client might not be the best solution, but could avoid repetitive code in the backend. 
         */
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
