using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models
{
    public class TeacherCourses
    {
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        public Teacher Teacher { get; set; }
        public Courses Courses { get; set; }
    }
}
