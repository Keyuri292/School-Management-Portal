using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models
{
    public class StudentCourse
    {
        [ForeignKey("StudentDetails")]
        public int SId { get; set; }
        public StudentDetails StudentDetails { get; set; }
        [ForeignKey("Courses")]
        public int Cid { get; set; }
        public Courses Courses { get; set; }
    }
}
