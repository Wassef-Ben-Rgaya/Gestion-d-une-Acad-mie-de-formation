using Core.Entities;


namespace Service.IService
{
    public  interface ICollegeService : ICollegeAsync<College, CollegeDto>
    {
        IQueryable<CollegeDto> GetCollege();
        Task<CollegeDto> GetCollege(int NumCollege);

        Task<IEnumerable<CollegeDto>> GetCollegesDisponible();
		

        /// Operation de MAJ        
        Task<bool> AddCollege(CollegeDto College);
        Task<bool> UpdCollege(CollegeDto College);
        Task<bool> delCollege(int CollegeId);
        Task<IEnumerable<CollegeDto>> GetAllColleges();



    }
}
