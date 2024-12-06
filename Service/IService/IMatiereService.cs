using Core.Entities;


namespace Service.IService
{
    public  interface IMatiereService : IMatiereAsync<Matiere, MatiereDto>
    {
        IQueryable<MatiereDto> GetMatiere();
        Task<MatiereDto> GetMatiere(int NumMatiere);

        Task<IEnumerable<MatiereDto>> GetMatieresDisponible();
		

        /// Operation de MAJ        
        Task<bool> AddMatiere(MatiereDto Matiere);
        Task<bool> UpdMatiere(MatiereDto Matiere);
        Task<bool> delMatiere(int MatiereId);
        Task<IEnumerable<MatiereDto>> GetAllMatieres();



    }
}
