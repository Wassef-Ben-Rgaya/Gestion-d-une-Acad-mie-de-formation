using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

using Service.IService;

namespace Service.Service
{
    public class EtudiantService : EtudiantAsync<Etudiant, EtudiantDto>, IEtudiantService
    {

        private readonly IRepositoryAsync<Etudiant> EtudiantRepository;
        private readonly IEtudiantAsync<Etudiant, EtudiantDto> srvEtudiant;
        private readonly IMapper mapper;

        private readonly ILogger _logger;





        public EtudiantService(IRepositoryAsync<Etudiant> EtudiantRepository,
             IEtudiantAsync<Etudiant, EtudiantDto> srvEtudiant,
             ILogger logger,
             IMapper mapper)
            : base(EtudiantRepository, mapper)
        {

            this.EtudiantRepository = EtudiantRepository;
            this.srvEtudiant = srvEtudiant;
            _logger = logger;
            this.mapper = mapper;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<EtudiantDto> GetEtudiant()
        {
            return this.srvEtudiant.GetAll();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EtudiantDto>> GetEtudiantsDisponible()
        {
            var Etud = await srvEtudiant.GetMuliple(
                  predicate: (i => i.EtudiantId != 0),
                  orderBy: (j => j.OrderByDescending(l => l.Nom)),
    
                  disableTracking: true

                );

            return Etud;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public async Task<bool> AddEtudiant(EtudiantDto Etudiant)
        {
            await srvEtudiant.Add(Etudiant);
            return true;

        }



        public async Task<EtudiantDto> GetEtudiant(int NumEtudiant)
        {
            var Etud = await srvEtudiant.GetFirstOrDefault(
                predicate: (i => i.EtudiantId == NumEtudiant),
                orderBy: (u => u.OrderBy(j => j.Nom)),
                disableTracking: true
                );

            return Etud;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public async Task<bool> UpdEtudiant(EtudiantDto Etudiant)
        {
            await srvEtudiant.Update(Etudiant);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public async Task<bool> delEtudiant(int etudiantid)
        {
            var Done = false;
            var EtudiantDto = await srvEtudiant.GetFirstOrDefault(predicate: (u => u.EtudiantId == etudiantid),
                                disableTracking: true);
            if (EtudiantDto != null)
            {
                await srvEtudiant.Delete(EtudiantDto.EtudiantId);
                Done = true;
            }
            return Done;
        }

        public Task<IEnumerable<EtudiantDto>> GetAllEtudiants()
        {
            try
            {
                var Etud = srvEtudiant.GetAll();

                return Task.FromResult<IEnumerable<EtudiantDto>>(Etud);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Etudiants: " + ex.Message);
                throw; // Propagez l'exception pour signaler qu'il y a eu une erreur lors de la récupération des articles.
            }
        }
    }
}