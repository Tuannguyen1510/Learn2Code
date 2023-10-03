using System;
using System.Collections.Generic;

namespace Learn2Code.Models;

public partial class Category
{
    public int Idcategory { get; set; }

    public string CategoryName { get; set; }

    public string CategoryDesc { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
