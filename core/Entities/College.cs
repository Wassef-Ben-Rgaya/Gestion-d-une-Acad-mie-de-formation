using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class College
{
    public int CollegeId { get; set; }

    public string Nom { get; set; }

    public string SiteWeb { get; set; }

    public virtual ICollection<Departement> Departements { get; set; } = new List<Departement>();

    public virtual ICollection<Etudiant> Etudiants { get; set; } = new List<Etudiant>();
}
