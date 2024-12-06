using AutoMapper;
using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service;

public partial class MatiereDto : IMapFrom<Matiere>
{
    public int MatiereId { get; set; }

    public string NomMatiere { get; set; }

    public int? EnseignantId { get; set; }

    public int? SalleId { get; set; }


    public virtual ICollection<Salle> Salle { get; } = new List<Salle>();
    public virtual ICollection<Enseignant> Enseignant { get; } = new List<Enseignant>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Matiere, MatiereDto>().ReverseMap();

    }
}
