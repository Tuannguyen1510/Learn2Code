using System;
using System.Collections.Generic;

namespace Learn2Code.Models;

public partial class Role
{
    public int Idrole { get; set; }

    public string NameRole { get; set; }

    public string Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
