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

        //Putting this here for now... perhaps would separate benefits/paycheck logic into its own controller/service if more functionality got added
        //[SwaggerOperation(Summary = "Get sample paycheck for employee by id")]
        //[HttpGet]
        //[Route("/paycheck/{id}")]
        //public async Task<ActionResult<ApiResponse>>

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
            var result = new ApiResponse<GetEmployeeDto>();
            try
            {
                var data = await _empService.UpdateEmployee(id, updatedEmployee);
                if(data is null)
                {
                    result.Success = false;
                    result.Message = "An error occurred when processing your request.";
                }
                else
                {
                    result.Success = true;
                    result.Data = data;
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Error = ex.ToString();
                result.Message = "An error occurred when processing your request.";

            }
            return result;
        }

        [SwaggerOperation(Summary = "Delete employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> DeleteEmployee(int id)
        {
            var result = new ApiResponse<GetEmployeeDto>();
            try
            {
                var deletedEmployee = await _empService.DeleteEmployee(id);
                if(deletedEmployee is null)
                {
                    result.Success = false;
                    result.Message = "There was an error processing your request.";
                }
                else
                {
                    result.Data = deletedEmployee;
                    result.Success = true;
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "There was an error processing your request.";
                result.Error = ex.ToString();
            }
            return result;
        }
    }
}
