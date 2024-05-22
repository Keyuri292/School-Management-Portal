using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models
{
    public class Courses
    {
        [Key]
        public int Id_Courses { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }

        public List<TeacherCourses> teacherCourses { get; set; }
        public List<StudentCourse> studentCourses { get; set;}

    }
}
