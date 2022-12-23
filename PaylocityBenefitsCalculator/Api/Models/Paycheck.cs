namespace Api.Models
{
    public class Paycheck
    {
        private Employee _employee;

        //for flexibility, set defaults but allow fees to be set through constructer if necessary?
        //could be a UI feature for admins to be able to adjust benefits fees.
        private decimal _monthlyBaseCost;
        private decimal _monthlyChargePerDep;

        //based on prompt's phrasing "over 50" i'm using > 50 not inclusive. Would clarify this
        //in real-world situation
        private decimal _monthlySurchargePerDepOver50;
        
  
        public decimal GrossPay { get { return CalculateGrossPerCheck(_employee.Salary); } }
        public decimal BaseCost { get { return CalculateBaseCostPerCheck(_monthlyBaseCost); } }
        public decimal DependentsTotalCost { get { return CalculateDependentsCostPerCheck((List<Dependent>)_employee.Dependents); } }
        public decimal Over80kSurcharge { get { return CalculateSurchargeSalaryOver80K(_employee.Salary); } }

        //UNCOMMENT THIS CONSTRUCTOR FOR TESTING. For in-app purposes, employee will be needed to initialize paycheck.
        public Paycheck(decimal monthlyBaseCost = 1000, decimal monthlyChargePerDep = 600, decimal monthlySurchargePerDepOver50 = 200)
        {
            _monthlyBaseCost = monthlyBaseCost;
            _monthlyChargePerDep = monthlyChargePerDep;
            _monthlySurchargePerDepOver50 = monthlySurchargePerDepOver50;
        }

        public Paycheck(Employee employee, decimal monthlyBaseCost = 1000, decimal monthlyChargePerDep = 600,
                        decimal monthlySurchargePerDepOverFifty = 200)
        {
            _employee = employee;
            _monthlyBaseCost = monthlyBaseCost;
            _monthlyChargePerDep = monthlyChargePerDep;
            _monthlySurchargePerDepOver50 = monthlySurchargePerDepOverFifty;
        }

        #region Calcuation Methods
        public decimal CalculateGrossPerCheck(decimal salary)
        {
            if (salary < 0) return 0;
            return Math.Round((salary / 26), 2);
        }

        public decimal CalculateBaseCostPerCheck(decimal monthlyBaseCost)
        {
            decimal result = (monthlyBaseCost * 12) / 26;
            return Math.Round(result, 2);
        }

        public decimal CalculateDependentsCostPerCheck(List<Dependent> dependents)
        {
            if (dependents.Count == 0) return 0;

            decimal sumResult = 0;
            var today = DateTime.Today;

            //calculate charges per paycheck (annualize then div by 26)
            decimal chargeDep = Math.Round((_monthlyChargePerDep * 12) / 26, 2);
            decimal surchargeOver50 = Math.Round((_monthlySurchargePerDepOver50 * 12) / 26, 2);

            foreach (Dependent dependent in dependents)
            {
                int age = today.Year - dependent.DateOfBirth.Year;
                if (age > 50) sumResult += (surchargeOver50 + chargeDep);
                else
                {
                    sumResult += chargeDep;
                }
                
            }
            return sumResult;
        }

        public decimal CalculateSurchargeSalaryOver80K(decimal salary)
        {
            return salary <= 80000.00M ? 0 : Math.Round((salary * 0.02M) / 26, 2);
        }

        #endregion
    }
}
