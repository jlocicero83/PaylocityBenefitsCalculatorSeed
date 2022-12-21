using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Dependent
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Column("RelationshipId")]
        public Relationship Relationship { get; set; }

        [Column("RelatedEmployeeId")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
