using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

using Service.IService;

namespace Service.Service
{
    public class DepartementService : DepartementAsync<Departement, DepartementDto>, IDepartementService
    {

        private readonly IRepositoryAsync<Departement> DepartementRepository;
        private readonly IDepartementAsync<Departement, DepartementDto> srvDepartement;
        private readonly IMapper mapper;

        private readonly ILogger _logger;





        public DepartementService(IRepositoryAsync<Departement> DepartementRepository,
             IDepartementAsync<Departement, DepartementDto> srvDepartement,
             ILogger logger,
             IMapper mapper)
            : base(DepartementRepository, mapper)
        {

            this.DepartementRepository = DepartementRepository;
            this.srvDepartement = srvDepartement;
            _logger = logger;
            this.mapper = mapper;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<DepartementDto> GetDepartement()
        {
            return this.srvDepartement.GetAll();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DepartementDto>> GetDepartementsDisponible()
        {
            var Dep= await srvDepartement.GetMuliple(
                  predicate: (i => i.DepartementId != 0),
                  orderBy: (j => j.OrderByDescending(l => l.NomDepartement)),
    

                  disableTracking: true

                );

            return Dep;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Departement"></param>
        /// <returns></returns>
        public async Task<bool> AddDepartement(DepartementDto Departement)
        {
            await srvDepartement.Add(Departement);
            return true;

        }



        public async Task<DepartementDto> GetDepartement(int NumDepartement)
        {
            var Dep = await srvDepartement.GetFirstOrDefault(
                predicate: (i => i.DepartementId == NumDepartement),
                orderBy: (u => u.OrderBy(j => j.NomDepartement)),
                disableTracking: true
                );
            return Dep;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Departement"></param>
        /// <returns></returns>
        public async Task<bool> UpdDepartement(DepartementDto Departement)
        {
            await srvDepartement.Update(Departement);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Departement"></param>
        /// <returns></returns>
        public async Task<bool> delDepartement(int departementid)
        {
            var Done = false;
            var DepartementDto = await srvDepartement.GetFirstOrDefault(predicate: (u => u.DepartementId == departementid),
                                disableTracking: true);
            if (DepartementDto != null)
            {
                await srvDepartement.Delete(DepartementDto.DepartementId);
                Done = true;
            }
            return Done;
        }

        public Task<IEnumerable<DepartementDto>> GetAllDepartements()
        {
            try
            {
                var Dep = srvDepartement.GetAll();

                return Task.FromResult<IEnumerable<DepartementDto>>(Dep);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les Departement : " + ex.Message);
                throw; // Propagez l'exception pour signaler qu'il y a eu une erreur lors de la récupération des articles.
            }
        }
    }
}