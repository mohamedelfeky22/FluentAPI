using System.Data.Entity;
namespace FluentAPI.Entities
{
    public class MyContext:DbContext
    {
        //DbModelBuilder is used to map CLR classes to a database schema.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
           // The default schema is dbo when the database is generated.
            modelBuilder.HasDefaultSchema("Admin");

           
            //Entity Splitting (Map Entity to Multiple Table)
            modelBuilder.Entity<Student>().Map(sd => {
                sd.Properties(p => new { p.ID, p.FirstMidName, p.LastName });
                sd.ToTable("StudentData");
            }).Map(si => {
                si.Properties(p => new { p.ID, p.EnrollmentDate });
                si.ToTable("StudentEnrollmentInfo");
            });
            //Map entity to table
            modelBuilder.Entity<Course>().ToTable("CourseDetail");
            modelBuilder.Entity<Enrollment>().ToTable("EnrollmentInfo");

            //rename the column name in student table from FirstMidName to FirstName as shown in the following code
            modelBuilder.Entity<Student>().Property(s => s.FirstMidName)
      .HasColumnName("FirstName");


            // Configure Primary Key
            //Note: Class defines a property whose name is “ID” or “Id” as primary key but if not has this convention we should configure it
            modelBuilder.Entity<Course>().HasKey<int>(C => C.CourseID);
            modelBuilder.Entity<Enrollment>().HasKey<int>(E => E.EnrollmentID);

            //Configure Column
            modelBuilder.Entity<Student>().Property(p => p.EnrollmentDate)
            .HasColumnName("EnDate")
            .HasColumnType("DateTime")
            .HasColumnOrder(2);

            //Configure MaxLength Property
            modelBuilder.Entity<Course>().Property(p => p.Title).HasMaxLength(24);

            //Configure Null or NotNull Property
            modelBuilder.Entity<Course>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<Student>().Property(p => p.EnrollmentDate).IsOptional();

            //Configure FK for one-to-many relationship
            modelBuilder.Entity<Enrollment>()

            .HasRequired<Student>(s => s.Student)
            .WithMany(t=> t.Enrollments)
            .HasForeignKey(u => u.StudentID);

            modelBuilder.Entity<Enrollment>()

            .HasRequired<Course>(C=> C.Course)
            .WithMany(t => t.Enrollments)
            .HasForeignKey(u => u.CourseID);


            // Configure many-to-many relationship
            //The default Code First conventions are used to create a join table when database is generated.
            //As a result, the StudentCourses table is created with Course_CourseID and Student_ID columns
            //modelBuilder.Entity<Student>()
            //.HasMany(s => s.Courses)
            //.WithMany(s => s.Students);

            //If you want to specify the join table name and the names of the columns in the table you need to do additional configuration by using the Map method.
            modelBuilder.Entity<Student>()

            .HasMany(s => s.Courses)
             .WithMany(s => s.Students)

            .Map(m=> {
                m.ToTable("StudentCoursesTable");
                m.MapLeftKey("StudentID");
                m.MapRightKey("CourseID");
            });

            // Configure ID as FK for StudentLogIn
            modelBuilder.Entity<Student>()

            .HasOptional(s=> s.StudentLogIn) //StudentLogIn is optional
            .WithRequired(t=> t.Student); // Create inverse relationship

        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentLogIn> StudentsLogIn { get; set; }
    }
}
