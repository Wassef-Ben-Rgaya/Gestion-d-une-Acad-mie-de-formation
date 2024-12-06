using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

using Service.IService;

namespace Service.Service
{
    public class SalleService : SalleAsync<Salle, SalleDto>, ISalleService
    {

        private readonly IRepositoryAsync<Salle> SalleRepository;
        private readonly ICollegeAsync<Salle, SalleDto> srvSalle;
        private readonly IMapper mapper;

        private readonly ILogger _logger;





        public SalleService(IRepositoryAsync<Salle> SalleRepository,
             ICollegeAsync<Salle, SalleDto> srvSalle,
             ILogger logger,
             IMapper mapper)
            : base(SalleRepository, mapper)
        {

            this.SalleRepository = SalleRepository;
            this.srvSalle = srvSalle;
            _logger = logger;
            this.mapper = mapper;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<SalleDto> GetSalle()
        {
            return this.srvSalle.GetAll();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SalleDto>> GetSallesDisponible()
        {
            var Sal = await srvSalle.GetMuliple(
                  predicate: (i => i.SalleId != 0),
                  orderBy: (j => j.OrderByDescending(l => l.NomSalle)),
                  
                  disableTracking: true

                );

            return Sal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Salle"></param>
        /// <returns></returns>
        public async Task<bool> AddSalle(SalleDto Salle)
        {
            await srvSalle.Add(Salle);
            return true;

        }



        public async Task<SalleDto> GetSalle(int NumSalle)
        {
            var Sal = await srvSalle.GetFirstOrDefault(
                predicate: (i => i.SalleId == NumSalle),
                orderBy: (u => u.OrderBy(j => j.NomSalle)),
           
                disableTracking: true
                );
            return Sal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Salle"></param>
        /// <returns></returns>
        public async Task<bool> UpdSalle(SalleDto Salle)
        {
            await srvSalle.Update(Salle);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Salle"></param>
        /// <returns></returns>
        public async Task<bool> delSalle(int Salleid)
        {
            var Done = false;
            var SalleDto = await srvSalle.GetFirstOrDefault(predicate: (u => u.SalleId == Salleid),
                                disableTracking: true);
            if (SalleDto != null)
            {
                await srvSalle.Delete(SalleDto.SalleId);
                Done = true;
            }
            return Done;
        }

        public Task<IEnumerable<SalleDto>> GetAllSalles()
        {
            try
            {
                var Sal = srvSalle.GetAll();

                return Task.FromResult<IEnumerable<SalleDto>>(Sal);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Salles: " + ex.Message);
                throw; // Propagez l'exception pour signaler qu'il y a eu une erreur lors de la récupération des Salle.
            }
        }
    }
}