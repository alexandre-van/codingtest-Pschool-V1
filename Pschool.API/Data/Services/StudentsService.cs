using Microsoft.EntityFrameworkCore;
using Pschool.API.Models.Entities;
using Pschool.API.Models.DTOs;

namespace Pschool.API.Data.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly PschoolDbContext _context;
        public StudentsService(PschoolDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            return await _context.Students
                .OrderBy(s => s.LastName)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Username = s.Username,
                    Email = s.Email,
                    HomeAddress = s.HomeAddress,
                    Phone1 = s.Phone1,
                    WorkPhone = s.WorkPhone,
                    HomePhone = s.HomePhone,
                    Siblings = s.Siblings,
                    ParentId = s.ParentId
                })
                .ToListAsync();
        }
        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }
        public async Task AddAsync(StudentDTO studentDTO)
        {
            var student = new Student
            {
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Username = studentDTO.Username,
                Email = studentDTO.Email,
                HomeAddress = studentDTO.HomeAddress,
                Phone1 = studentDTO.Phone1,
                WorkPhone = studentDTO.WorkPhone,
                HomePhone = studentDTO.HomePhone,
                Siblings = studentDTO.Siblings
            };
            _context.Students.Add(student);
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
        public async Task UpdateAsync(int id, StudentDTO studentDTO)
        {
            var student = await GetByIdAsync(id);

            student.FirstName = studentDTO.FirstName ?? student.FirstName;
            student.LastName = studentDTO.LastName ?? student.LastName;
            student.Email = studentDTO.Email ?? student.Email;
            student.HomeAddress = studentDTO.HomeAddress ?? student.HomeAddress;
            student.Phone1 = studentDTO.Phone1 ?? student.Phone1;
            student.WorkPhone = studentDTO.WorkPhone ?? student.WorkPhone;
            student.HomePhone = studentDTO.HomePhone ?? student.HomePhone;
            student.Siblings = studentDTO.Siblings ?? student.Siblings;

            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var student = await GetByIdAsync(id);
            if (student == null)
                return false;

            var parent = await _context.Parents.FindAsync(student.ParentId);
            if (parent != null)
            {
                _context.Parents.Remove(parent);
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StudentDTO> GetStudentByParentAsync(int parentId)
        {
            var student = await _context.Students
                .Where(s => s.ParentId == parentId)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Username = s.Username,
                    Email = s.Email,
                    HomeAddress = s.HomeAddress,
                    Phone1 = s.Phone1,
                    WorkPhone = s.WorkPhone,
                    HomePhone = s.HomePhone,
                    Siblings = s.Siblings,
                    ParentId = s.ParentId
                }).FirstOrDefaultAsync();
            return student;
        }
    }
}