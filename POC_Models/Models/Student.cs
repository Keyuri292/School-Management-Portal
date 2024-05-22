using POC_Models.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name_Student { get; set; }
        public string Email_Student { get; set; }
        public string Phone_Student { get; set; }

        public StudentDetails StudentDetails { get; set; }
        
    }
}
