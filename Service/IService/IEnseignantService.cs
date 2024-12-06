using Core.Entities;


namespace Service.IService
{
    public  interface IEnseignantService : IEnseignantAsync<Enseignant, EnseignantDto>
    {
        IQueryable<EnseignantDto> GetEnseignant();
        Task<EnseignantDto> GetEnseignant(int NumEnseignant);

        Task<IEnumerable<EnseignantDto>> GetEnseignantsDisponible();
		

        /// Operation de MAJ        
        Task<bool> AddEnseignant(EnseignantDto Enseignant);
        Task<bool> UpdEnseignant(EnseignantDto Enseignant);
        Task<bool> delEnseignant(int EnseignantId);
        Task<IEnumerable<EnseignantDto>> GetAllEnseignants();



    }
}
