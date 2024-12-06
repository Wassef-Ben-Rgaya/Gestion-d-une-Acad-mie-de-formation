using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Salle
{
    public int SalleId { get; set; }

    public string NomSalle { get; set; }

    public int? Nbdeplace { get; set; }

    public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
}
