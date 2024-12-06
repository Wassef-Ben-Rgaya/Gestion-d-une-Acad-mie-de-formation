using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

using Service.IService;
using System.Net;

namespace Service.Service
{
    public class EnseignantService : EnseignantAsync<Enseignant, EnseignantDto>, IEnseignantService
    {

        private readonly IRepositoryAsync<Enseignant> EnseignantRepository;
        private readonly IEnseignantAsync<Enseignant, EnseignantDto> srvEnseignant;
        private readonly IMapper mapper;

        private readonly ILogger _logger;





        public EnseignantService(IRepositoryAsync<Enseignant> EnseignantRepository,
             IEnseignantAsync<Enseignant, EnseignantDto> srvEnseignant,
             ILogger logger,
             IMapper mapper)
            : base(EnseignantRepository, mapper)
        {

            this.EnseignantRepository = EnseignantRepository;
            this.srvEnseignant = srvEnseignant;
            _logger = logger;
            this.mapper = mapper;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<EnseignantDto> GetEnseignant()
        {
            return this.srvEnseignant.GetAll();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EnseignantDto>> GetEnseignantsDisponible()
        {
            var Ens = await srvEnseignant.GetMuliple(
                  predicate: (i => i.EnseignantId != 0),
                  orderBy: (j => j.OrderByDescending(l => l.Nom)),
    
                  disableTracking: true
            );

            return Ens;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Enseignant"></param>
        /// <returns></returns>
        public async Task<bool> AddEnseignant(EnseignantDto Enseignant)
        {
            await srvEnseignant.Add(Enseignant);
            return true;

        }



        public async Task<EnseignantDto> GetEnseignant(int NumEnseignant)
        {
            var Ens = await srvEnseignant.GetFirstOrDefault(
                predicate: (i => i.EnseignantId == NumEnseignant),
                orderBy: (u => u.OrderBy(j => j.Nom)),
                disableTracking: true
                );

            return Ens;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Enseignant"></param>
        /// <returns></returns>
        public async Task<bool> UpdEnseignant(EnseignantDto Enseignant)
        {
            await srvEnseignant.Update(Enseignant);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Enseignant"></param>
        /// <returns></returns>
        public async Task<bool> delEnseignant(int Enseignantid)
        {
            var Done = false;
            var EnseignantDto = await srvEnseignant.GetFirstOrDefault(predicate: (u => u.EnseignantId == Enseignantid),
                                disableTracking: true);
            if (EnseignantDto != null)
            {
                await srvEnseignant.Delete(EnseignantDto.EnseignantId);
                Done = true;
            }
            return Done;
        }

        public Task<IEnumerable<EnseignantDto>> GetAllEnseignants()
        {
            try
            {
                var Ens = srvEnseignant.GetAll();

                return Task.FromResult<IEnumerable<EnseignantDto>>(Ens);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Enseignants: " + ex.Message);
                throw; // Propagez l'exception pour signaler qu'il y a eu une erreur lors de la récupération des Enseignants.
            }
        }
    }
}