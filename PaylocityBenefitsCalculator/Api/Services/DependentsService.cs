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

        public async Task<List<GetDependentDto>?> GetDependentByRelatedEmployeeId(int id)
        {
            var dependents = await _dbContext.Dependent.Where(dep => dep.EmployeeId == id).ToListAsync();
            if (dependents is null) return null;
            else
            {
                List<GetDependentDto?> results = new List<GetDependentDto?>();
                foreach(Dependent dep in dependents)
                {
                    results.Add(DtoMapper.MapDependentToDto(dep));
                }
                return results;
            }
        }

        #endregion

        #region PUT Methods

        public async Task<GetDependentDto?> UpdateDependent(int id, UpdateDependentDto updatedDependent)
        {
            var existing = await _dbContext.Dependent.SingleOrDefaultAsync(dep => dep.Id == id);
            if (existing is null || updatedDependent is null) return null;
            else
            {
                //update existing record and save changes
                existing.FirstName = updatedDependent.FirstName;
                existing.LastName = updatedDependent.LastName;
                existing.DateOfBirth = updatedDependent.DateOfBirth;

                //TODO: may need to check if employee already has spouse/partner the update is trying to add a second.
                existing.Relationship = updatedDependent.Relationship;

                _dbContext.Update(existing);
                int recordsUpdated = await _dbContext.SaveChangesAsync();
                return recordsUpdated == 1 ? DtoMapper.MapDependentToDto(existing) : null;
            }
        }

        #endregion

        #region POST Methods

        //trying this as a sync method so a more detailed message can be returned in the Api response re: Spouse/Partner limit = 1 using ref.
        //There may be a better way...
        public AddDependentDto? AddDependentForEmployee(int employeeId, AddDependentDto addDependentDto, ref ApiResponse<AddDependentDto> response)
        {
            if (employeeId == 0 || addDependentDto is null) return null;

            //if new dependent is spouse/partner, check if employee already has one.
            if(addDependentDto.Relationship == Relationship.Spouse || addDependentDto.Relationship == Relationship.DomesticPartner)
            {
                var alreadyHasSpouseOrPartner = EmployeeHasSpouseOrPartner(employeeId);
                if (alreadyHasSpouseOrPartner)
                {
                    response.Message = "This employee already has a dependent spouse or domestic partner.";
                    return null;
                }
            }

            //if not, check to see if an identical dependent record exists.
            var existed = _dbContext.Dependent.Where(dep => dep.LastName == addDependentDto.LastName
                                                        && dep.DateOfBirth.Equals(addDependentDto.DateOfBirth))
                                              .FirstOrDefault();
            if(existed is null)
            {
                Dependent newDependent = DtoMapper.MapDtoToDependent(addDependentDto);
                _dbContext.Dependent.Add(newDependent);
            }
            return addDependentDto;

        }

        #endregion

        #region DELETE Methods

        public async Task<GetDependentDto?> DeleteDependent(int id)
        {
            var dependent = await _dbContext.Dependent.FindAsync(id);
            if (dependent is null) return null;

            _dbContext.Dependent.Remove(dependent);
            int recordsRemoved = _dbContext.SaveChanges();
            return recordsRemoved == 0 ? null : DtoMapper.MapDependentToDto(dependent);
        }

        #endregion

        private bool EmployeeHasSpouseOrPartner(int employeeId)
        {
            if (employeeId == 0) return false;

            var dependents = _dbContext.Dependent.Where(dep => dep.EmployeeId == employeeId).ToList();
            if (dependents.Any())
            {
                foreach(Dependent dep in dependents)
                {
                    if (dep.Relationship == Relationship.Spouse || dep.Relationship == Relationship.DomesticPartner) return true;
                }
            }
            return false;
        }
    }
}
