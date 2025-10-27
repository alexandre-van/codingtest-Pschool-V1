using Pschool.API.Models.Entities;
using Pschool.API.Models.DTOs;

namespace Pschool.API.Data.Services
{
    public interface IParentsService
    {
        Task<IEnumerable<ParentDTO>> GetAllAsync();
        Task<Parent> GetByIdAsync(int id);
        Task AddAsync(ParentDTO parentDTO);
        Task<bool> AddParentAndStudentAsync(ParentAndStudentDTO parentAndStudentDTO);
        Task UpdateAsync(int id, ParentDTO parentDTO);
        Task<bool> DeleteAsync(int id);
    }
}