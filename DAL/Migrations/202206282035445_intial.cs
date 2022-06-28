namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Admin.CourseDetail",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 24),
                        Credits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "Admin.EnrollmentInfo",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("Admin.CourseDetail", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("Admin.StudentData", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "Admin.StudentData",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Admin.StudentLogIns",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        EmailID = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Admin.StudentData", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "Admin.StudentCoursesTable",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentID, t.CourseID })
                .ForeignKey("Admin.StudentData", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("Admin.CourseDetail", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "Admin.StudentEnrollmentInfo",
                c => new
                    {
                        EnDate = c.DateTime(),
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Admin.StudentData", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.StudentEnrollmentInfo", "ID", "Admin.StudentData");
            DropForeignKey("Admin.EnrollmentInfo", "StudentID", "Admin.StudentData");
            DropForeignKey("Admin.StudentLogIns", "ID", "Admin.StudentData");
            DropForeignKey("Admin.StudentCoursesTable", "CourseID", "Admin.CourseDetail");
            DropForeignKey("Admin.StudentCoursesTable", "StudentID", "Admin.StudentData");
            DropForeignKey("Admin.EnrollmentInfo", "CourseID", "Admin.CourseDetail");
            DropIndex("Admin.StudentEnrollmentInfo", new[] { "ID" });
            DropIndex("Admin.StudentCoursesTable", new[] { "CourseID" });
            DropIndex("Admin.StudentCoursesTable", new[] { "StudentID" });
            DropIndex("Admin.StudentLogIns", new[] { "ID" });
            DropIndex("Admin.EnrollmentInfo", new[] { "StudentID" });
            DropIndex("Admin.EnrollmentInfo", new[] { "CourseID" });
            DropTable("Admin.StudentEnrollmentInfo");
            DropTable("Admin.StudentCoursesTable");
            DropTable("Admin.StudentLogIns");
            DropTable("Admin.StudentData");
            DropTable("Admin.EnrollmentInfo");
            DropTable("Admin.CourseDetail");
        }
    }
}
