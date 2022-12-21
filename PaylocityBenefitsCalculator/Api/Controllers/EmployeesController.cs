﻿using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private EmployeesService _empService;
        public EmployeesController(EmployeesService empService)
        {
            _empService = empService;
        }

        [SwaggerOperation(Summary = "Get employee by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
        {
            throw new NotImplementedException();
        }

        [SwaggerOperation(Summary = "Get all employees")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
        {
            var result = new ApiResponse<List<GetEmployeeDto>>();
            try
            {
                List<GetEmployeeDto> data = _empService.GetAllEmployees();
                result.Data = data;
                result.Success = data.Any();
            }
            catch(Exception ex)
            {
                result.Error = ex.ToString();
                result.Message = ex.Message;
            }
            return result;
            //task: use a more realistic production approach
            //var employees = new List<GetEmployeeDto>
            //{
            //    new()
            //    {
            //        Id = 1,
            //        FirstName = "LeBron",
            //        LastName = "James",
            //        Salary = 75420.99m,
            //        DateOfBirth = new DateTime(1984, 12, 30)
            //    },
            //    new()
            //    {
            //        Id = 2,
            //        FirstName = "Ja",
            //        LastName = "Morant",
            //        Salary = 92365.22m,
            //        DateOfBirth = new DateTime(1999, 8, 10),
            //        Dependents = new List<GetDependentDto>
            //        {
            //            new()
            //            {
            //                Id = 1,
            //                FirstName = "Spouse",
            //                LastName = "Morant",
            //                Relationship = Relationship.Spouse,
            //                DateOfBirth = new DateTime(1998, 3, 3)
            //            },
            //            new()
            //            {
            //                Id = 2,
            //                FirstName = "Child1",
            //                LastName = "Morant",
            //                Relationship = Relationship.Child,
            //                DateOfBirth = new DateTime(2020, 6, 23)
            //            },
            //            new()
            //            {
            //                Id = 3,
            //                FirstName = "Child2",
            //                LastName = "Morant",
            //                Relationship = Relationship.Child,
            //                DateOfBirth = new DateTime(2021, 5, 18)
            //            }
            //        }
            //    },
            //    new()
            //    {
            //        Id = 3,
            //        FirstName = "Michael",
            //        LastName = "Jordan",
            //        Salary = 143211.12m,
            //        DateOfBirth = new DateTime(1963, 2, 17),
            //        Dependents = new List<GetDependentDto>
            //        {
            //            new()
            //            {
            //                Id = 4,
            //                FirstName = "DP",
            //                LastName = "Jordan",
            //                Relationship = Relationship.DomesticPartner,
            //                DateOfBirth = new DateTime(1974, 1, 2)
            //            }
            //        }
            //    }
            //};

            //var result = new ApiResponse<List<GetEmployeeDto>>
            //{
            //    Data = employees,
            //    Success = true
            //};

            //return result;
        }

        [SwaggerOperation(Summary = "Add employee")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<AddEmployeeDto>>>> AddEmployee(AddEmployeeDto newEmployee)
        { 
            throw new NotImplementedException();
        }

        [SwaggerOperation(Summary = "Update employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            throw new NotImplementedException();
        }

        [SwaggerOperation(Summary = "Delete employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }
    }
}
