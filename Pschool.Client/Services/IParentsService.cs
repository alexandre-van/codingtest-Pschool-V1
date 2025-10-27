using Pschool.Client.Models;

namespace Pschool.Client.Services
{
    public interface IParentsService
    {
        Task<bool> CreateParentAndStudentAsync(ParentAndStudentDTO parentAndStudent);
        Task<List<ParentDTO>> GetListParentsAsync();
        Task<ParentDTO> GetParentAsync(int parentId);
        Task<bool> EditParentAsync(int parentId, ParentDTO parentDTO);
        Task<bool> DeleteParentAsync(int parentId);
    }
}