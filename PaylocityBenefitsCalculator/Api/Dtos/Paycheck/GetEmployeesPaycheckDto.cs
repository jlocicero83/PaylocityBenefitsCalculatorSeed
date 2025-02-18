﻿using Api.Dtos.Employee;

namespace Api.Dtos.Paycheck
{
    public class GetEmployeesPaycheckDto
    {
        public int EmployeeId { get; set; }
        //Maybe better to pass the employee DTO here...
        //public GetEmployeeDto Employee { get; set; }
        public decimal Salary { get; set; }
        public decimal GrossPay { get; set; }
        public decimal BaseCost { get; set; }
        public decimal DependentsTotalCost { get; set; }
        public decimal Over80kSurcharge { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }
    }
}
