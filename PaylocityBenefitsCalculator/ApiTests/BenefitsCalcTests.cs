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
        [InlineData(-1, 0)]
        public void Gross_Should_Be_Salary_DivBy_26(decimal salary, decimal expected)
        {
            //Arrange
            Paycheck paycheck = new Paycheck();

            //Act
            decimal actual = paycheck.CalculateGrossPerCheck(salary);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1000, 461.54)]
        public void Monthly_BaseCost_Should_Be_Annualized_Then_DivBy_26(decimal monthlyBase, decimal expected)
        {
            //Arrange
            Paycheck paycheck = new Paycheck();

            //Act
            decimal actual = paycheck.CalculateBaseCostPerCheck(monthlyBase);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(DependentsChargeTestData))]
        public void Cost_For_Dependents_Should_Consider_Age(List<Dependent> dependents, decimal expected)
        {
            //Arrange
            Paycheck paycheck = new Paycheck();


            //Act
            decimal actual = paycheck.CalculateDependentsCost(dependents);

            //Assert
            Assert.Equal(expected, actual);
        }

        //Test Dependents Data - xUnit is new for me, so there may be (hopefully) a more elegant way of writing this
        public static IEnumerable<object[]> DependentsChargeTestData =>
            new List<object[]>
            {
                new object[] 
                { 
                    new List<Dependent>() 
                    { 
                        //dep under 50 - should not include surcharge
                        new Dependent() { DateOfBirth = new DateTime(2000, 1, 1) },
                        
                        //dep 50 years old - should not incur surcharge
                        new Dependent() {DateOfBirth = new DateTime(1972, 1, 1)},

                        //dep over 50 - should incur surcharge
                        new Dependent() {DateOfBirth = new DateTime(1971, 1, 1)}
                    }, 
                    //Expected Result
                    923.07 
                },          
            };
    }
           
    
}
