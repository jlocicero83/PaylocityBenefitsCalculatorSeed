using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Utilities
{
    public static class DtoMapper
    {
        /*I'm not too familiar with the DTO pattern - I added this util to handle the frequent mapping for models -> dtos. There is an AutoMapper library
         that could be used in place of this, but for the purposes of this project I decided to implement manually for now. 
         */
        public static GetDependentDto MapDependentToDto(Dependent dependent)
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

        public static GetEmployeeDto MapEmployeeToDto(Employee employee)
        {
            GetEmployeeDto employeeDto = new GetEmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth
            };
            foreach (Dependent dependent in employee.Dependents)
            {
                employeeDto.Dependents.Add(MapDependentToDto(dependent));
            }
            return employeeDto;

        }
    }
}
