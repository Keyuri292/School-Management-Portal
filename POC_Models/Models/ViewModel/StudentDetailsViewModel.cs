using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models.ViewModel
{
    public class StudentDetailsViewModel
    {
        [Key]
        public int Id_Student { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int GradeId { get; set; }
        public List<Courses> Courses { get; set; }
        public List<int> SelectedCourseIds { get; set; }
    }

}
