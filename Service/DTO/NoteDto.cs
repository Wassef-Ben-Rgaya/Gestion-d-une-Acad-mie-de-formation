using AutoMapper;
using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Service;

public partial class NoteDto : IMapFrom<Note>
{
    public int? EtudiantId { get; set; }

    public int? MatiereId { get; set; }

    public decimal? Ds { get; set; }

    public decimal? Tp { get; set; }

    public decimal? AutreNote { get; set; }

    public decimal? Examan { get; set; }



    public virtual ICollection<Etudiant> Etudiant { get; } = new List<Etudiant>();

    public virtual ICollection<Matiere> Matiere { get; } = new List<Matiere>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Note, NoteDto>().ReverseMap();

    }
}
