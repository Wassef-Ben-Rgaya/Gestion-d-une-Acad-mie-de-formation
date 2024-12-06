using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

using Service.IService;

namespace Service.Service
{
    public class MatiereService : MatiereAsync<Matiere, MatiereDto>, IMatiereService
    {

        private readonly IRepositoryAsync<Matiere> MatiereRepository;
        private readonly IMatiereAsync<Matiere, MatiereDto> srvMatiere;
        private readonly IMapper mapper;

        private readonly ILogger _logger;





        public MatiereService(IRepositoryAsync<Matiere> MatiereRepository,
             IMatiereAsync<Matiere, MatiereDto> srvMatiere,
             ILogger logger,
             IMapper mapper)
            : base(MatiereRepository, mapper)
        {

            this.MatiereRepository = MatiereRepository;
            this.srvMatiere = srvMatiere;
            _logger = logger;
            this.mapper = mapper;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<MatiereDto> GetMatiere()
        {
            return this.srvMatiere.GetAll();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MatiereDto>> GetMatieresDisponible()
        {
            var Mat= await srvMatiere.GetMuliple(
                  predicate: (i => i.MatiereId != 0),
                  orderBy: (j => j.OrderByDescending(l => l.NomMatiere)),
    

                  disableTracking: true

                );

            return Mat;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Matiere"></param>
        /// <returns></returns>
        public async Task<bool> AddMatiere(MatiereDto Matiere)
        {
            await srvMatiere.Add(Matiere);
            return true;

        }



        public async Task<MatiereDto> GetMatiere(int NumMatiere)
        {
            var Mat = await srvMatiere.GetFirstOrDefault(
                predicate: (i => i.MatiereId == NumMatiere),
                orderBy: (u => u.OrderBy(j => j.NomMatiere)),
                disableTracking: true
                );
            return Mat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Matiere"></param>
        /// <returns></returns>
        public async Task<bool> UpdMatiere(MatiereDto Matiere)
        {
            await srvMatiere.Update(Matiere);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Salle"></param>
        /// <returns></returns>
        public async Task<bool> delMatiere(int Matiereid)
        {
            var Done = false;
            var MatiereDto = await srvMatiere.GetFirstOrDefault(predicate: (u => u.MatiereId == Matiereid),
                                disableTracking: true);
            if (MatiereDto != null)
            {
                await srvMatiere.Delete(MatiereDto.MatiereId);
                Done = true;
            }
            return Done;
        }

        public Task<IEnumerable<MatiereDto>> GetAllMatieres()
        {
            try
            {
                var Mat = srvMatiere.GetAll();

                return Task.FromResult<IEnumerable<MatiereDto>>(Mat);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Matieres : " + ex.Message);
                throw; // Propagez l'exception pour signaler qu'il y a eu une erreur lors de la récupération des Matieres.
            }
        }
    }
}