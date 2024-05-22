using POC_Models.Models;

namespace POC_Models.Models.ViewModel
{
    public class AddStudentCourseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int GradeId { get; set; }
        public List<Courses> AvailableCourses { get; set; }
        public int[] SelectedCourseIds { get; set; }
    }
}
