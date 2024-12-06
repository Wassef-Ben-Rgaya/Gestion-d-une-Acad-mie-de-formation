using AutoMapper;
using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service;

public partial class EnseignantDto : IMapFrom<Enseignant>
{
    public int EnseignantId { get; set; }

    public int? DepartementId { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string NumeroTelephone { get; set; }

    public string Email { get; set; }

    public string Mdp { get; set; }

    public DateOnly? Dateemploie { get; set; }

    public int? Indice { get; set; }

    public virtual ICollection<Departement> Departements { get; set; } = new List<Departement>();

    public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Enseignant, EnseignantDto>().ReverseMap();

    }
}
