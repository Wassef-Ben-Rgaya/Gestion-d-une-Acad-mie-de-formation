using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Matiere
{
    public int MatiereId { get; set; }

    public string NomMatiere { get; set; }

    public int? EnseignantId { get; set; }

    public int? SalleId { get; set; }

    public virtual Enseignant Enseignant { get;}

    public virtual Salle Salle { get;}
}
