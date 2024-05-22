using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models
{
    public class Teacher
    {
        [Key]
        public int Id_Teacher { get; set; }
        public string Name_Teacher { get; set; }
        public string Email_Teacher { get; set; }
        public string Phone_Teacher { get; set; }
        public List<TeacherCourses> teacherCourses { get; set; }
    }
}
