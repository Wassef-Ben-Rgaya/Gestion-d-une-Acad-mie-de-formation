using AutoMapper;
using Core.Entities;
using Service;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service;

public partial class DepartementDto : IMapFrom<Departement>
{
    public int DepartementId { get; set; }

    public int? CollegeId { get; set; }

    public string NomDepartement { get; set; }

    public int? ResDepartementId { get; set; }



    public virtual ICollection<Enseignant> Enseignants { get; } = new List<Enseignant>();

    public virtual ICollection<College> College { get; } = new List<College>();

    public virtual ICollection<Enseignant> ResDepartement { get;  } = new List<Enseignant>();


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Departement, DepartementDto>().ReverseMap();

    }
}
