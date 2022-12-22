using Api.Dtos.Dependent;
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
            var result = new ApiResponse<GetEmployeeDto>();
            try
            {
               var data = await _empService.GetEmployeeById(id);
               if(data is null)
                {
                    result.Success = false;
                    result.Message = "No data was returned.";
                }
                else
                {
                    result.Data = data; 
                    result.Success = true;
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Error = ex.ToString();
                result.Message = ex.Message;
            }
            return result;
        }

        [SwaggerOperation(Summary = "Get all employees")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
        {
            var result = new ApiResponse<List<GetEmployeeDto>>();
            try
            {
                List<GetEmployeeDto> data = await _empService.GetAllEmployees();
                result.Data = data;
                if (data.Any()) result.Success = true;
                else
                {
                    result.Success = false;
                    result.Message = "No data was returned.";
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Error = ex.ToString();
                result.Message = ex.Message;
            }
            return result;
            //task: use a more realistic production approach: created a SQL db for test data.
        }

        [SwaggerOperation(Summary = "Add employee")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<AddEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee)
        { 
            var result = new ApiResponse<AddEmployeeDto>();
            try
            {
                var data = await _empService.AddNewEmployee(newEmployee);
                if (data is null)
                {
                    result.Success = false;
                    result.Message = "There was an error when completing your request.";
                }
                else
                {
                    result.Data = data;
                    result.Success = true;
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Error=ex.ToString();
                result.Message = ex.Message;
            }
            return result;
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
