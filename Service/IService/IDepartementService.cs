using Core.Entities;


namespace Service.IService
{
    public  interface IDepartementService : IDepartementAsync<Departement, DepartementDto>
    {
        IQueryable<DepartementDto> GetDepartement();
        Task<DepartementDto> GetDepartement(int NumDepartement);

        Task<IEnumerable<DepartementDto>> GetDepartementsDisponible();
		

        /// Operation de MAJ        
        Task<bool> AddDepartement(DepartementDto Departement);
        Task<bool> UpdDepartement(DepartementDto Departement);
        Task<bool> delDepartement(int DepartementId);
        Task<IEnumerable<DepartementDto>> GetAllDepartements();



    }
}
