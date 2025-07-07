using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;
using NotesAPI.Repositories;

namespace NotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _repository;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INoteRepository repository, ILogger<NotesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetAll([FromQuery] string? search)
        {
            var notes = _repository.GetAll(search);
            if (!notes.Any()) return NoContent();
            return Ok(notes);
        }

        [HttpGet("{id}")]

        public ActionResult<Note> GetById(int id)
        {
            var note = _repository.GetById(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]

        public ActionResult<Note> Add([FromBody] Note note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.Add(note);
            _logger.LogInformation("Note added: " + System.Text.Json.JsonSerializer.Serialize(note));
            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, [FromBody] Note updatedNote)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.Exists(id))
                return NotFound();

            updatedNote.Id = id;
            _repository.Update(updatedNote);

            _logger.LogInformation("Note updated: " + System.Text.Json.JsonSerializer.Serialize(updatedNote));
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            if (!_repository.Exists(id))
                return NotFound();

            _repository.Delete(id);
            _logger.LogInformation("Note delete: {Id} " + id);
            return NoContent();
        }
    }
}
