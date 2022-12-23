namespace Api.Models
{
    public class Paycheck
    {
        public Employee Employee { get; set; }
        private int employeeAge
        {
            get
            {
                var todaysDate = DateTime.Today;
                return todaysDate.Year - Employee.DateOfBirth.Year;
            }
        }

        //Every employee incurs a 1K per month base cost
        public readonly decimal MonthlyBaseCost = 1000M;

        //Each dependent represents $600 per month for benefits
        public decimal DependentsCost { get
            {
                return (decimal)600 * Employee.Dependents.Count();
            } 
        }

        //if salary > 80K, incur an additional 2% of salary in costs
        public decimal SalaryOver80kCost
        {
            get
            {
                return Employee.Salary > 80000.00M ? (Employee.Salary * 0.02M) : 0;
            }
        }

        public decimal AgeOverFiftyCost
        {
            get
            {
                return employeeAge > 50 ? 200.00M : 0;
            }
        }

        public decimal TotalDeductions { get { return MonthlyBaseCost + DependentsCost + SalaryOver80kCost + AgeOverFiftyCost; } }

        public decimal MonthlyGross
        {
            get
            {
                return (Employee.Salary / 26);
            }
        }

        public decimal MonthlyNet
        {
            get
            {
                return MonthlyGross - TotalDeductions;
            }
        }

        public Paycheck(Employee employee)
        {
            Employee = employee;
        }






    }
}
