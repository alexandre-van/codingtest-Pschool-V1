using Pschool.API.Models.DTOs;
using Pschool.API.Models.Entities;

namespace Pschool.API.Data.Services
{
    public interface IStudentsService
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(StudentDTO studentDTO);
        Task<bool> AddParentAndStudentAsync(ParentAndStudentDTO parentAndStudentDTO);
        Task UpdateAsync(int id, StudentDTO studentDTO);
        Task<bool> DeleteAsync(int id);
        Task<StudentDTO> GetStudentByParentAsync(int parentId);
    }
}