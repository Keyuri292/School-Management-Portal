using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Models.Models
{
    public class Grades
    {
        [Key]
        public int Id { get; set; }
        public string Grade {  get; set; }
        public List<StudentDetails> StudentDetails { get; set; }
    }
}
