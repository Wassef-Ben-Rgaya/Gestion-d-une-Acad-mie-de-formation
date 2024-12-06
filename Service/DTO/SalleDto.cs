using AutoMapper;
using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service;

public partial class SalleDto : IMapFrom<Salle>
{
    public int SalleId { get; set; }

    public string NomSalle { get; set; }

    public int? Nbdeplace { get; set; }

    public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Salle, SalleDto>().ReverseMap();

    }
}
