using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual StudentLogIn StudentLogIn { get; set; }
    }
}
