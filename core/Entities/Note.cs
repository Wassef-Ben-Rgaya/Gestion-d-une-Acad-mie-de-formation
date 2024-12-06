using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Note
{
    public int EtudiantId { get; set; }

    public int MatiereId { get; set; }

    public decimal? Ds { get; set; }

    public decimal? Tp { get; set; }

    public decimal? AutreNote { get; set; }

    public decimal? Examan { get; set; }

    public virtual Etudiant Etudiant { get;  }

    public virtual Matiere Matiere { get; }
}
