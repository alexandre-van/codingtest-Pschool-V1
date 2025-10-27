using System.ComponentModel.DataAnnotations;

namespace Pschool.API.Models.Entities
{
    public class Parent
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

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}