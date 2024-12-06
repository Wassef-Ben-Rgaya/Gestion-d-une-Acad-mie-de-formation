using Core.Entities;


namespace Service.IService
{
    public  interface IEtudiantService : IEtudiantAsync<Etudiant, EtudiantDto>
    {
        IQueryable<EtudiantDto> GetEtudiant();
        Task<EtudiantDto> GetEtudiant(int NumEtudiant);

        Task<IEnumerable<EtudiantDto>> GetEtudiantsDisponible();
		

        /// Operation de MAJ        
        Task<bool> AddEtudiant(EtudiantDto Etudiant);
        Task<bool> UpdEtudiant(EtudiantDto Etudiant);
        Task<bool> delEtudiant(int EtudiantId);
        Task<IEnumerable<EtudiantDto>> GetAllEtudiants();



    }
}
