using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models.ViewModel
{
    public class CourseViewModel
    {
        public int Id_Course { get; set; }
        public string Name_Course { get; set; }
        public string Description_Course { get; set; }
        public List<int> SelectedTeacherIds { get; set; } 
        public List<Teacher> AvailableTeachers { get; set; }
        public List<StudentDetails> AvailableStudents { get; set; }
        public List<int> SelectedStudentIds { get; set; }
    }
}
