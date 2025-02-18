﻿using Api.Models;

namespace Api.Dtos.Dependent
{
    public class AddDependentDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }

        //moving here since I can't see a circumstance where we'd add a dependent without a matching employee
        public int EmployeeId { get; set; }
    }
}
