using Microsoft.EntityFrameworkCore;
using Pschool.API.Models.Entities;
using Pschool.API.Models.DTOs;

namespace Pschool.API.Data.Services
{
    public class ParentsService : IParentsService
    {
        private readonly PschoolDbContext _context;
        public ParentsService(PschoolDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ParentDTO>> GetAllAsync()
        {
            return await _context.Parents
                .OrderBy(p => p.LastName)
                .Select(p => new ParentDTO
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Username = p.Username,
                    Email = p.Email,
                    HomeAddress = p.HomeAddress,
                    Phone1 = p.Phone1,
                    WorkPhone = p.WorkPhone,
                    HomePhone = p.HomePhone,
                    Siblings = p.Siblings
                })
                .ToListAsync();
        }
        public async Task<Parent> GetByIdAsync(int id)
        {
            return await _context.Parents.FindAsync(id);
        }
        public async Task AddAsync(ParentDTO parentDTO)
        {
            var parent = new Parent
            {
                FirstName = parentDTO.FirstName,
                LastName = parentDTO.LastName,
                Username = parentDTO.Username,
                Email = parentDTO.Email,
                HomeAddress = parentDTO.HomeAddress,
                Phone1 = parentDTO.Phone1,
                WorkPhone = parentDTO.WorkPhone,
                HomePhone = parentDTO.HomePhone,
                Siblings = parentDTO.Siblings
            };
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> AddParentAndStudentAsync(ParentAndStudentDTO parentAndStudentDTO)
        {
            // Multiple db operations
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var parent = new Parent
                {
                    FirstName = parentAndStudentDTO.Parent.FirstName,
                    LastName = parentAndStudentDTO.Parent.LastName,
                    Username = parentAndStudentDTO.Parent.Username,
                    Email = parentAndStudentDTO.Parent.Email,
                    HomeAddress = parentAndStudentDTO.Parent.HomeAddress,
                    Phone1 = parentAndStudentDTO.Parent.Phone1,
                    WorkPhone = parentAndStudentDTO.Parent.WorkPhone,
                    HomePhone = parentAndStudentDTO.Parent.HomePhone,
                    Siblings = parentAndStudentDTO.Parent.Siblings
                };
                _context.Parents.Add(parent);
                await _context.SaveChangesAsync();

                var student = new Student
                {
                    FirstName = parentAndStudentDTO.Student.FirstName,
                    LastName = parentAndStudentDTO.Student.LastName,
                    Username = parentAndStudentDTO.Student.Username,
                    Email = parentAndStudentDTO.Student.Email,
                    HomeAddress = parentAndStudentDTO.Student.HomeAddress,
                    Phone1 = parentAndStudentDTO.Student.Phone1,
                    WorkPhone = parentAndStudentDTO.Student.WorkPhone,
                    HomePhone = parentAndStudentDTO.Student.HomePhone,
                    Siblings = parentAndStudentDTO.Student.Siblings,
                    ParentId = parent.Id
                };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task UpdateAsync(int id, ParentDTO parentDTO)
        {
            var parent = await GetByIdAsync(id);

            parent.FirstName = parentDTO.FirstName ?? parent.FirstName;
            parent.LastName = parentDTO.LastName ?? parent.LastName;
            parent.Email = parentDTO.Email ?? parent.Email;
            parent.HomeAddress = parentDTO.HomeAddress ?? parent.HomeAddress;
            parent.Phone1 = parentDTO.Phone1 ?? parent.Phone1;
            parent.WorkPhone = parentDTO.WorkPhone ?? parent.WorkPhone;
            parent.HomePhone = parentDTO.HomePhone ?? parent.HomePhone;
            parent.Siblings = parentDTO.Siblings ?? parent.Siblings;

            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
                return false;

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}