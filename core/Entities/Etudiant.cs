using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Etudiant
{
    public int EtudiantId { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string NumeroTelephone { get; set; }

    public string Email { get; set; }

    public string Mdp { get; set; }

    public int? Anneentree { get; set; }

    public int? CollegeId { get; set; }

    public virtual College College { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
