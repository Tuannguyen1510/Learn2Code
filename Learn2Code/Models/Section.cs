using System;
using System.Collections.Generic;

namespace Learn2Code.Models;

public partial class Section
{
    public int Idsection { get; set; }

    public int? Idlession { get; set; }

    public string Titel { get; set; }

    public string TxtContent { get; set; }

    public string Image { get; set; }

    public virtual Lession IdlessionNavigation { get; set; }
}
