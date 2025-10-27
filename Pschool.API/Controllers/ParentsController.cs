using Microsoft.AspNetCore.Mvc;
using Pschool.API.Data.Services;
using Pschool.API.Models.DTOs;
using Pschool.API.Models.Entities;

namespace Pschool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentsController : ControllerBase
    {
        private readonly IParentsService _service;

        public ParentsController(IParentsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParent(int id)
        {
            var parent = await _service.GetByIdAsync(id);
            if (parent == null)
                return NotFound();

            return Ok(parent);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var listParents = await _service.GetAllAsync();
            if (listParents == null)
                return NotFound();

            return Ok(listParents);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParentDTO parentDTO)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(parentDTO);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("with-student")]
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
        public async Task<IActionResult> Update(int id, [FromBody] ParentDTO parentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parent = await _service.GetByIdAsync(id);
            if (parent == null)
                return NotFound();

            await _service.UpdateAsync(id, parentDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}