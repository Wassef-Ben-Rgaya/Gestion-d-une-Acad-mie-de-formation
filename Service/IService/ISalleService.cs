using Core.Entities;


namespace Service.IService
{
    public  interface ISalleService : ISalleAsync<Salle, SalleDto>
    {
        IQueryable<SalleDto> GetSalle();
        Task<SalleDto> GetSalle(int NumSalle);

        Task<IEnumerable<SalleDto>> GetSallesDisponible();
		

        /// Operation de MAJ        
        Task<bool> AddSalle(SalleDto Salle);
        Task<bool> UpdSalle(SalleDto Salle);
        Task<bool> delSalle(int SalleId);
        Task<IEnumerable<SalleDto>> GetAllSalles();



    }
}
