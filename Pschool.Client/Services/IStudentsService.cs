using Pschool.Client.Models;

namespace Pschool.Client.Services
{
    public interface IStudentsService
    {
        Task<bool> CreateParentAndStudentAsync(ParentAndStudentDTO studentDTO);
        Task<List<StudentDTO>> GetListStudentsAsync();
        Task<StudentDTO> GetStudentAsync(int studentId);
        Task<bool> EditStudentAsync(int studentId, StudentDTO studentDTO);
        Task<bool> DeleteStudentAsync(int studentId);
    }
}