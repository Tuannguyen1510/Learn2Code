using System;
using System.Collections.Generic;

namespace Learn2Code.Models;

public partial class User
{
    
    public int Iduser { get; set; }

    public string UserName { get; set; }

    public string PassWord { get; set; }

    public string Email { get; set; }

    public string Avatar { get; set; }

    public int? Idrole { get; set; }

    public bool IsActive { get; set; }

    public string Contact { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Role IdroleNavigation { get; set; }

    public virtual ICollection<Moderator> Moderators { get; set; } = new List<Moderator>();
}
