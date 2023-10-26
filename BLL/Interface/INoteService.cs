using BLL.DTO;

namespace BLL.Interface
{
    public interface INoteService
    {
        Task AddNoteAsync(NoteDTO note);
        Task<NoteDTO> GetNoteAsync(int id);
        Task DeleteNoteByIdAsync(int id);
        Task UpdateNoteAsync(NoteDTO note);

    }
}