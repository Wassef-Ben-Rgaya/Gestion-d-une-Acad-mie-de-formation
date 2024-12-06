using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Enseignant
{
    public int EnseignantId { get; set; }

    public int? DepartementId { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string NumeroTelephone { get; set; }

    public string Email { get; set; }

    public string Mdp { get; set; }

    public DateOnly? Dateemploie { get; set; }

    public string Indice { get; set; }

    public virtual Departement Departement { get; set; }

    public virtual ICollection<Departement> Departements { get; set; } = new List<Departement>();

    public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
}
