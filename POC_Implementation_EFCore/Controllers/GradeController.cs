using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POC_DataAccess.Data;
using POC_Models.Models;
using System.Data.SqlClient;
using System.Linq;

namespace POC_Implementation_EFCore.Controllers
{
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? gradeId)
        {
            List<StudentDetails> students;

            if (gradeId.HasValue && gradeId != 0)
            {
                var parameter = new SqlParameter("@GradeId", gradeId);
                students = _context.StudentDetails
                                   .FromSqlRaw("EXEC GetStudentsByGrade @GradeId", parameter)
                                   .ToList();
                if (students.Count == 0)
                {
                    ViewBag.NoStudents = true;
                }
            }
            else
            {
                students = _context.StudentDetails.ToList();
            }

            var grades = _context.Grades.ToList();
            ViewBag.Grades = new SelectList(grades, "Id", "Grade");

            return View(students);
        }

    }
}
