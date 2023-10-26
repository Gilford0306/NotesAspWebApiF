using AutoMapper;
using BLL.DTO;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using NotesAspWeb.Model;

namespace WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class NoteController : ControllerBase
    {
        private INoteService _noteService;


        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost("Created")]
        public async Task<ActionResult> CreateNote(NoteDTO noteDTO)
        {
            await _noteService.AddNoteAsync(noteDTO);
            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<NoteDTO>> GetByIdAsync(int id)
        {
            return Ok(await _noteService.GetNoteAsync(id));
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<NoteViewModel>> GetAll()
        {
            IEnumerable<NoteDTO> noteDtos = (IEnumerable<NoteDTO>)_noteService.GetNoteALL();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NoteDTO, NoteViewModel>()).CreateMapper();
            var notes = mapper.Map<IEnumerable<NoteDTO>, List<NoteViewModel>>(noteDtos);
            return notes;
        }

        [HttpPut("Update")]
        public async Task<ActionResult<NoteDTO>> UpdateNoteAsync(NoteDTO noteDTO)
        {
            await _noteService.UpdateNoteAsync(noteDTO);
            return Ok();
        }


        [HttpDelete("Deleted")]
        public async Task<ActionResult<NoteDTO>> DeletedNoteAsync(int id)
        {
            await _noteService.DeleteNoteByIdAsync(id);
            return Ok();
        }
    }
}