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

        public static GetEmployeeDto MapEmployeeToGetDto(Employee employee)
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

        public static AddEmployeeDto MapEmployeeToAddDto(Employee employee)
        {
            var result = new AddEmployeeDto()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth
            };
            if (employee.Dependents.Count == 0) return result;
            else
            {
                result.Dependents = new List<AddDependentDto>();
                foreach (Dependent dependent in employee.Dependents)
                {
                    result.Dependents.Add(MapDependentToAddDto(dependent));
                }
            }
            return result;
        }

        public static AddDependentDto MapDependentToAddDto(Dependent dependent)
        {
            return new AddDependentDto()
            {
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                DateOfBirth = dependent.DateOfBirth,
                Relationship = dependent.Relationship
            };
        }

        public static Employee MapDtoToEmployee(AddEmployeeDto addEmployeeDto)
        {
            Employee result = new Employee()
            {
                FirstName = addEmployeeDto.FirstName,
                LastName = addEmployeeDto.LastName,
                Salary = addEmployeeDto.Salary,
                DateOfBirth = addEmployeeDto.DateOfBirth
            };
            
            return result;
        }

        public static Employee MapDtoToEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            Employee result = new Employee()
            {
                FirstName = updateEmployeeDto.FirstName,
                LastName = updateEmployeeDto.LastName,
                Salary = updateEmployeeDto.Salary,
            };

            return result;
        }

        public static Dependent MapDtoToDependent(AddDependentDto addDependentDto)
        {
            return new Dependent()
            {
                FirstName = addDependentDto.FirstName,
                LastName = addDependentDto.LastName,
                DateOfBirth = addDependentDto.DateOfBirth,
                Relationship = addDependentDto.Relationship,
                EmployeeId = addDependentDto.EmployeeId
            };
        }
    }
}
