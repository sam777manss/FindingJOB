using System;
using System.Collections.Generic;

namespace Colleges.DBModels
{
    public partial class University
    {
        public University()
        {
            CourseUniversities = new HashSet<CourseUniversity>();
            StudentUniversities = new HashSet<StudentUniversity>();
        }

        public string? Name { get; set; }
        public int Id { get; set; }

        public virtual ICollection<CourseUniversity> CourseUniversities { get; set; }
        public virtual ICollection<StudentUniversity> StudentUniversities { get; set; }
    }
}
