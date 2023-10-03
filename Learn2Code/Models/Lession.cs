using System;
using System.Collections.Generic;

namespace Learn2Code.Models;

public partial class Lession
{
    public int Idlession { get; set; }

    public int? Idcourse { get; set; }

    public string Titel { get; set; }

    public string LessionDesc { get; set; }

    public string Sort { get; set; }

    public virtual Course IdcourseNavigation { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
