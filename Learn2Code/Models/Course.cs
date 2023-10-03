using System;
using System.Collections.Generic;

namespace Learn2Code.Models;

public partial class Course
{
    public int Idcourse { get; set; }

    public int? Idcategory { get; set; }

    public string CourseDesc { get; set; }

    public string CourseName { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Category IdcategoryNavigation { get; set; }

    public virtual ICollection<Lession> Lessions { get; set; } = new List<Lession>();
}
