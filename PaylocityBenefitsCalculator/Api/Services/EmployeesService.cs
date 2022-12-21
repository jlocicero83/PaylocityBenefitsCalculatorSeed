using Api.DataAccess;

namespace Api.Services
{
    public class EmployeesService
    {
        private BenefitsCalcDbContext _dbContext;

        //Inject db context through constructor
        public EmployeesService(BenefitsCalcDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
