using System;
using System.Collections.Generic;

namespace Learn2Code.Models;

public partial class Moderator
{
    public int Idmoderator { get; set; }

    public int? Iduser { get; set; }

    public virtual User IduserNavigation { get; set; }
}
