using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

using Service.IService;

namespace Service.Service
{
    public class CollegeService : CollegeAsync<College, CollegeDto>, ICollegeService
    {

        private readonly IRepositoryAsync<College> CollegeRepository;
        private readonly ICollegeAsync<College, CollegeDto> srvCollege;
        private readonly IMapper mapper;

        private readonly ILogger _logger;





        public CollegeService(IRepositoryAsync<College> CollegeRepository,
             ICollegeAsync<College, CollegeDto> srvCollege,
             ILogger logger,
             IMapper mapper)
            : base(CollegeRepository, mapper)
        {

            this.CollegeRepository = CollegeRepository;
            this.srvCollege = srvCollege;
            _logger = logger;
            this.mapper = mapper;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<CollegeDto> GetCollege()
        {
            return this.srvCollege.GetAll();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CollegeDto>> GetCollegesDisponible()
        {
            var Coll = await srvCollege.GetMuliple(
                  predicate: (i => i.CollegeId != 0),
                  orderBy: (j => j.OrderByDescending(l => l.Nom)),
                  include: (h => h
                  .Include(f => f.Departements) ),
                  disableTracking: true

                );

            return Coll;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>
        public async Task<bool> AddCollege(CollegeDto College)
        {
            await srvCollege.Add(College);
            return true;

        }



        public async Task<CollegeDto> GetCollege(int NumCollege)
        {
            var Coll = await srvCollege.GetFirstOrDefault(
                predicate: (i => i.CollegeId == NumCollege),
                orderBy: (u => u.OrderBy(j => j.Nom)),
                include: (h => h
                  .Include(f => f.Departements)),
                disableTracking: true
                );
            return Coll;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>
        public async Task<bool> UpdCollege(CollegeDto College)
        {
            await srvCollege.Update(College);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>
        public async Task<bool> delCollege(int Collegeid)
        {
            var Done = false;
            var CollegeDto = await srvCollege.GetFirstOrDefault(predicate: (u => u.CollegeId == Collegeid),
                                disableTracking: true);
            if (CollegeDto != null)
            {
                await srvCollege.Delete(CollegeDto.CollegeId);
                Done = true;
            }
            return Done;
        }

        public Task<IEnumerable<CollegeDto>> GetAllColleges()
        {
            try
            {
                var Coll = srvCollege.GetAll();

                return Task.FromResult<IEnumerable<CollegeDto>>(Coll);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors de la récupération de tous les College: " + ex.Message);
                throw; // Propagez l'exception pour signaler qu'il y a eu une erreur lors de la récupération des articles.
            }
        }
    }
}