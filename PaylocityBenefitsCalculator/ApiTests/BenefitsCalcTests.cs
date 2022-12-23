using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests
{
    public class BenefitsCalcTests
    {
        [Theory]
        [InlineData(80000.00, 3076.92)]
        [InlineData(75420.99, 2900.81)]
        [InlineData(92365.22, 3552.51)]
        [InlineData(143211.12, 5508.12)]
        public void Monthly_Gross_Should_Be_Salary_DivBy_26(decimal salary, decimal expected)
        {
            //Arrange
            Paycheck paycheck = new Paycheck();

            //Act
            decimal actual = paycheck.CalculateMonthlyGross(salary);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
