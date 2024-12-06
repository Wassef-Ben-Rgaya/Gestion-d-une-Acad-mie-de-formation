using AutoMapper;
using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service;

public partial class EtudiantDto : IMapFrom<Etudiant>
{
    public int EtudiantId { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string NumeroTelephone { get; set; }

    public string Email { get; set; }

    public string Mdp { get; set; }

    public int? Anneentree { get; set; }

    public int? CollegeId { get; set; }
    public virtual ICollection<Note> Notes { get; } = new List<Note>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Etudiant, EtudiantDto>().ReverseMap();

    }
}
