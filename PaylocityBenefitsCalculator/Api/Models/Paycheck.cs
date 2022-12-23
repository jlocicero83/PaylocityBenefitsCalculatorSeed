namespace Api.Models
{
    public class Paycheck
    {
        private Employee _employee;
        public Paycheck(Employee employee)
        {
            _employee = employee;
        }
        public decimal MonthlyGross { get { return CalculateMonthlyGross(_employee.Salary); } }

        public decimal CalculateMonthlyGross(decimal salary)
        {
            return Math.Round((salary / 26), 2);
        }
    }
}
