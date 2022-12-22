using Api.DataAccess;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class DependentsService
    {
        private BenefitsCalcDbContext _dbContext;

        public DependentsService(BenefitsCalcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region GET Methods

        public async Task<List<GetDependentDto>> GetAllDependents()
        {
            List<GetDependentDto> results = new List<GetDependentDto>();

            var dependents = await _dbContext.Dependent.ToListAsync();
            foreach(Dependent dependent in dependents)
            {
                results.Add(DtoMapper.MapDependentToDto(dependent));
            }
            return results;
        }

        public async Task<GetDependentDto?> GetDependentById(int id)
        {
            var dependent = await _dbContext.Dependent.SingleOrDefaultAsync(dep => dep.Id == id);
            if (dependent is null) return null;
            else
            {
                return DtoMapper.MapDependentToDto(dependent);
            }
        }

        #endregion
    }
}
