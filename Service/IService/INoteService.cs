using Core.Entities;


namespace Service.IService
{
    public  interface INoteService : INoteAsync<Note, NoteDto>
    {
        IQueryable<NoteDto> GetNote();
        Task<NoteDto> GetNote(int NumMatiere);

        Task<IEnumerable<NoteDto>> GetNotesDisponible();
		

        /// Operation de MAJ        
        Task<bool> AddNote(NoteDto Note);
        Task<bool> UpdNote(NoteDto Note);
        Task<bool> delNote(int MatiereId);
        Task<IEnumerable<NoteDto>> GetAllNotes();



    }
}
