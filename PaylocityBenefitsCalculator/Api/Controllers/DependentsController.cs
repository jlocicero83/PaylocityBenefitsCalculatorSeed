using Api.Dtos.Dependent;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DependentsController : ControllerBase
    {
        private DependentsService _depService;

        public DependentsController(DependentsService depService)
        {
            _depService = depService;
        }

        [SwaggerOperation(Summary = "Get dependent by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
        {
            var result = new ApiResponse<GetDependentDto>();
            try
            {
                var data = await _depService.GetDependentById(id);
                if (data is null)
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
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.ToString();
                result.Message = ex.Message;
            }
            return result;
        }

        [SwaggerOperation(Summary = "Get all dependents")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
        {
            var result = new ApiResponse<List<GetDependentDto>>();
            try
            {
                List<GetDependentDto> data = await _depService.GetAllDependents();
                result.Data = data;
                if (data.Any()) result.Success = true;
                else
                {
                    result.Success = false;
                    result.Message = "No data was returned.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.ToString();
                result.Message = ex.Message;
            }
            return result;
        }

        [SwaggerOperation(Summary = "Add dependent")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<AddDependentDto>>>> AddDependent(AddDependentDto newDependent)
        {
            throw new NotImplementedException();
        }

        [SwaggerOperation(Summary = "Update dependent")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> UpdateDependent(int id, UpdateDependentDto updatedDependent)
        {
            throw new NotImplementedException();
        }

        [SwaggerOperation(Summary = "Delete dependent")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> DeleteDependent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
