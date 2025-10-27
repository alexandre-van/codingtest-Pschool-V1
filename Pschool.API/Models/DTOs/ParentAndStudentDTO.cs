namespace Pschool.API.Models.DTOs
{
    public class ParentAndStudentDTO
    {
        public ParentDTO Parent { get; set; } = null!;
        public StudentDTO Student { get; set; } = null!;
    }
}