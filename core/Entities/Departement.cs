using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Departement
{
    public int DepartementId { get; set; }

    public int? CollegeId { get; set; }

    public string NomDepartement { get; set; }

    public int? ResDepartementId { get; set; }

    public virtual College College { get; }

    public virtual ICollection<Enseignant> Enseignants { get;  } = new List<Enseignant>();

    public virtual Enseignant ResDepartement { get; }
}
