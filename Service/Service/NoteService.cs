using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

using Service.IService;

namespace Service.Service
{
    public class NoteService : NoteAsync<Note, NoteDto>, INoteService
    {

        private readonly IRepositoryAsync<Note> NoteRepository;
        private readonly INoteAsync<Note, NoteDto> srvNote;
        private readonly IMapper mapper;

        private readonly ILogger _logger;





        public NoteService(IRepositoryAsync<Note> NoteRepository,
             INoteAsync<Note, NoteDto> srvNote,
             ILogger logger,
             IMapper mapper)
            : base(NoteRepository, mapper)
        {

            this.NoteRepository = NoteRepository;
            this.srvNote = srvNote;
            _logger = logger;
            this.mapper = mapper;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<NoteDto> GetNote()
        {
            return this.srvNote.GetAll();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NoteDto>> GetNotesDisponible()
        {
            var Not = await srvNote.GetMuliple(
                  predicate: (i => i.MatiereId!= 0),
                  orderBy: (j => j.OrderByDescending(l => l.Matiere)),
    
                  disableTracking: true

                );

            return Not;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Note"></param>
        /// <returns></returns>
        public async Task<bool> AddNote(NoteDto Note)
        {
            await srvNote.Add(Note);
            return true;

        }



        public async Task<NoteDto> GetNote(int NumMatiere)
        {
            var Not = await srvNote.GetFirstOrDefault(
                predicate: (i => i.MatiereId == NumMatiere),
                orderBy: (u => u.OrderBy(j => j.Matiere)),

                disableTracking: true
                );
            return Not;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Note"></param>
        /// <returns></returns>
        public async Task<bool> UpdNote(NoteDto Note)
        {
            await srvNote.Update(Note);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Note"></param>
        /// <returns></returns>
        public async Task<bool> delNote(int Noteid)
        {
            var Done = false;
            var NoteDto = await srvNote.GetFirstOrDefault(predicate: (u => u.MatiereId == Noteid),
                                disableTracking: true);
            if (NoteDto != null)
            {
                await srvNote.Delete(NoteDto.MatiereId);
                Done = true;
            }
            return Done;
        }

        public Task<IEnumerable<NoteDto>> GetAllNotes()
        {
            try
            {
                var Not = srvNote.GetAll();

                return Task.FromResult<IEnumerable<NoteDto>>(Not);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Notes: " + ex.Message);
                throw; // Propagez l'exception pour signaler qu'il y a eu une erreur lors de la récupération des Notes.
            }
        }
    }
}