using Microsoft.AspNetCore.Mvc;
using Pschool.API.Data.Services;
using Pschool.API.Models.DTOs;
using Pschool.API.Models.Entities;

namespace Pschool.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _service;

        public StudentsController(IStudentsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _service.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var listStudents = await _service.GetAllAsync();
            if (listStudents == null)
                return NotFound();

            return Ok(listStudents);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(studentDTO);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("with-parent")]
        public async Task<IActionResult> CreateParentAndStudent([FromBody] ParentAndStudentDTO parentAndStudentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddParentAndStudentAsync(parentAndStudentDTO);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = await _service.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            await _service.UpdateAsync(id, studentDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpGet("by-parent/{id}")]
        public async Task<IActionResult> GetStudentByParent(int id)
        {
            var student = await _service.GetStudentByParentAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }
    }
}