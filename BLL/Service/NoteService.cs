using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interface;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;


namespace BLL.Service
{
    public class NoteService : INoteService
    {
        private NotesContext _context;


        public NoteService(NotesContext context)
        {
            _context = context;
        }


        public async Task AddNoteAsync(NoteDTO note)
        {
            if (note is null)
            {
                throw new ValidationException("You tying add empty note", "");
            }

            await _context.Notes.AddAsync(new Note() { Title = note.Title, Text = note.Text });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteByIdAsync(int id)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note is null)
            {
                throw new ArgumentNullException("note is null");
            }

            _context.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task DisposeAsync() => await _context.DisposeAsync();

        public async Task<NoteDTO> GetNoteAsync(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note is null)
            {
                throw new ValidationException("note is null", "");
            }

            return new NoteDTO { Id = note.Id, Title = note.Title, Text = note.Text};
        }

        public async Task UpdateNoteAsync(NoteDTO note)
        {
            Note findedNote = await _context.Notes.FirstOrDefaultAsync(x => x.Id == note.Id);

            if (findedNote is null)
            {
                throw new ArgumentNullException("Note did not found");
            }

            findedNote.Title = note.Title;
            findedNote.Text = note.Text;

            _context.Update(findedNote);

            await _context.SaveChangesAsync();
        }

        public IEnumerable<NoteDTO> GetNoteALL()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Note, NoteDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Note>, List<NoteDTO>>(_context.Notes.ToList());
        }
    }
}
