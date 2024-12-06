using AutoMapper;
using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service;

public partial class CollegeDto : IMapFrom<College>
{
    public int CollegeId { get; set; }

    public string Nom { get; set; }

    public string SiteWeb { get; set; }

    public virtual ICollection<Etudiant> Etudiants { get; } = new List<Etudiant>();

    public virtual ICollection<Departement> Departements { get; set; } = new List<Departement>();


    public void Mapping(Profile profile)
    {
        profile.CreateMap<College, CollegeDto>().ReverseMap();

    }
}
