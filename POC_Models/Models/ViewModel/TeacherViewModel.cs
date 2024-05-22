using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models.ViewModel
{
    public class TeacherViewModel
    {
        public int Id_Teacher { get; set; }
        public string Name_Teacher { get; set; }
        public string Email_Teacher { get; set; }
        public string Phone_Teacher { get; set; }
        public List<int> SelectedCourseIds { get; set; } // List of selected course ids
        public List<Courses> AvailableCourses { get; set; }
    }
}
