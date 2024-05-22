using Microsoft.EntityFrameworkCore;
using POC_Models.Models;
using static Azure.Core.HttpHeader;
using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace POC_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentDetails> StudentDetails { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Grades> Grades { get; set; }
        public DbSet<TeacherCourses> TeacherCourses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            //One to one
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentDetails)
                .WithOne(sd => sd.Student)
                .HasForeignKey<StudentDetails>(sd => sd.StudentId);

            //One to many
            modelBuilder.Entity<StudentDetails>().HasOne(z => z.Grade).WithMany(z => z.StudentDetails).HasForeignKey(z => z.GradeId);

            //Many to many
            modelBuilder.Entity<TeacherCourses>().HasKey(b => new { b.CourseId, b.TeacherId });
            modelBuilder.Entity<TeacherCourses>().HasOne(z => z.Courses).WithMany(z => z.teacherCourses).HasForeignKey(z => z.CourseId);
            modelBuilder.Entity<TeacherCourses>().HasOne(z => z.Teacher).WithMany(z => z.teacherCourses).HasForeignKey(z => z.TeacherId);

            modelBuilder.Entity<StudentCourse>().HasKey(b => new { b.SId, b.Cid });
            modelBuilder.Entity<StudentCourse>().HasOne(z => z.StudentDetails).WithMany(z => z.studentCourses).HasForeignKey(z => z.SId);
            modelBuilder.Entity<StudentCourse>().HasOne(z => z.Courses).WithMany(z => z.studentCourses).HasForeignKey(z => z.Cid);


            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = 1,
                Name_Student = "Student 1",
                Email_Student = "1@email.com",
                Phone_Student = "1236547890"
            },
            new Student
            {
                Id = 2,
                Name_Student = "Student 2",
                Email_Student = "2@email.com",
                Phone_Student = "9874563210"
            });
            modelBuilder.Entity<StudentDetails>().HasData(new StudentDetails
            {
                Id_Student = 1,
                StudentId = 1,
                firstName_Student = "Student",
                lastName_Student =  "1",
                Email_Student = "1@email.com",
                Phone_Student = "1236547890",
                FatherName_Student = "F1",
                MotherName_Student = "M1",
                GradeId = 1
            },
            new StudentDetails 
            {
                Id_Student = 2,
                StudentId = 2,
                firstName_Student = "Student",
                lastName_Student = "2",
                Email_Student = "2@email.com",
                Phone_Student = "9874563210",
                FatherName_Student = "F2",
                MotherName_Student = "M2",
                GradeId = 1
            });
            
            modelBuilder.Entity<Grades>().HasData(
                new Grades
                {
                    Id = 1,
                    Grade="A"
                },
                new Grades
                {
                    Id = 2,
                    Grade = "B"
                },
                new Grades
                {
                    Id = 3,
                    Grade = "C"
                },
                new Grades
                {
                    Id = 4,
                    Grade = "D"
                },
                new Grades
                {
                    Id = 5,
                    Grade = "E"
                }
                );

            modelBuilder.Entity<Teacher>().HasData(
                
                new Teacher
                {
                    Id_Teacher = 1,
                    Name_Teacher = "Teacher 1",
                    Email_Teacher = "1@email.com",
                    Phone_Teacher = "985632147"
                
                },
                new Teacher
                {
                    Id_Teacher = 2,
                    Name_Teacher = "Teacher 2",
                    Email_Teacher = "2@email.com",
                    Phone_Teacher = "9512364780"

                }
                );
            
        }
    }
}
