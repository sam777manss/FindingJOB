using System;
using System.Collections.Generic;

namespace Colleges.DBModels
{
    public partial class Course
    {
        public Course()
        {
            CourseUniversities = new HashSet<CourseUniversity>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public string? Name { get; set; }
        public int Id { get; set; }

        public virtual ICollection<CourseUniversity> CourseUniversities { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
