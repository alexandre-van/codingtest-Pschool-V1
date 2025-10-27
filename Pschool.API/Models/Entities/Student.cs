using System.ComponentModel.DataAnnotations;

namespace Pschool.API.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        public string? Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? Email { get; set; }
        public string? HomeAddress { get; set; }
        [RegularExpression(@"^\+?[1-9]\d{1,14}$")]
        public string? Phone1 { get; set; }
        [RegularExpression(@"^\+?[1-9]\d{1,14}$")]
        public string? WorkPhone { get; set; }
        [RegularExpression(@"^\+?[1-9]\d{1,14}$")]
        public string? HomePhone { get; set; }
        [Required(ErrorMessage = "Siblings are required")]
        public int? Siblings { get; set; }

//        public ICollection<Parent> Parents { get; set; } = new List<Parent>();
        // Foreign Key
        [Required]
        public int ParentId { get; set; }

        public Parent Parent { get; set; } = null!;
    }
}