using System;
using System.Collections.Generic;

namespace Colleges.DBModels
{
    public partial class Student
    {
        public Student()
        {
            StudentCourses = new HashSet<StudentCourse>();
            StudentUniversities = new HashSet<StudentUniversity>();
        }

        public string? Name { get; set; }
        public int Id { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentUniversity> StudentUniversities { get; set; }
    }
}
