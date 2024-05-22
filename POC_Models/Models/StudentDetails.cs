using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POC_Models.Models
{
    public class StudentDetails
    {
        [Key]
        public int Id_Student { get; set; }
        
        public string firstName_Student { get; set; }
        public string lastName_Student { get; set; }

        public string Email_Student { get; set; }
        public string Phone_Student { get; set; }
        public string FatherName_Student { get; set; }
        public string MotherName_Student { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{firstName_Student} {lastName_Student}";
            }
        }
        
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Grades")]
        public int GradeId { get; set; }
        public Grades Grade { get; set; }

        public List<StudentCourse> studentCourses { get; set; }
    }
}
