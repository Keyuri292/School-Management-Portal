using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POC_DataAccess.Data;
using POC_Models.Models;
using POC_Models.Models.ViewModel;
using System.Diagnostics;

namespace POC_Implementation_EFCore.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Student> students = _applicationDbContext.Students.Include(s => s.StudentDetails);

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                    s.Name_Student.Contains(searchString) ||
                    s.Email_Student.Contains(searchString) ||
                    s.Phone_Student.Contains(searchString)
                );
            }

            var data = await students.ToListAsync();

            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var studentDetails = await _applicationDbContext.StudentDetails
                .Include(sd => sd.Student)
                .Include(sd => sd.Grade) 
                .FirstOrDefaultAsync(sd => sd.Id_Student == id);

            if (studentDetails == null)
            {
                return NotFound();
            }


            var courses = await _applicationDbContext.StudentCourses
                    .Include(sc => sc.Courses)
                    .ThenInclude(c => c.teacherCourses) 
                    .ThenInclude(tc => tc.Teacher) 
                    .Where(sc => sc.SId == studentDetails.StudentId)
                    .Select(sc => sc.Courses)
                    .ToListAsync();

            var viewModel = new StudentdetailsViewModel2
            {
                StudentDetails = studentDetails,
                StudentCourses = courses
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new AddStudentCourseViewModel
            {
                AvailableCourses = _applicationDbContext.Courses.ToList()
            };
            ViewBag.Grades = _applicationDbContext.Grades.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentCourseViewModel viewModel)
        {
            var student = new Student
            {
                Name_Student = viewModel.FirstName + " " + viewModel.LastName,
                Email_Student = viewModel.Email,
                Phone_Student = viewModel.Phone
            };

            _applicationDbContext.Students.Add(student);
            await _applicationDbContext.SaveChangesAsync();

            var studentDetails = new StudentDetails
            {
                StudentId = student.Id,
                firstName_Student = viewModel.FirstName,
                lastName_Student = viewModel.LastName,
                Email_Student = viewModel.Email,
                Phone_Student = viewModel.Phone,
                FatherName_Student = viewModel.FatherName,
                MotherName_Student = viewModel.MotherName,
                GradeId = viewModel.GradeId
            };

            _applicationDbContext.StudentDetails.Add(studentDetails);
            await _applicationDbContext.SaveChangesAsync();

            var selectedCourseIds = viewModel.SelectedCourseIds;
            if (selectedCourseIds != null && selectedCourseIds.Any())
            {
                foreach (var courseId in selectedCourseIds)
                {
                    _applicationDbContext.StudentCourses.Add(new StudentCourse { SId = studentDetails.Id_Student, Cid = courseId });
                }
                await _applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _applicationDbContext.Students
                .Include(s => s.StudentDetails)
                .ThenInclude(sd => sd.studentCourses)
                .FirstOrDefaultAsync(s => s.Id == id);

            ViewBag.Grades = _applicationDbContext.Grades.ToList();


            if (student == null)
            {
                return NotFound();
            }

            var studentCourses = await _applicationDbContext.StudentCourses
                .Where(sc => sc.SId == student.StudentDetails.StudentId)
                .Select(sc => sc.Courses)
                .ToListAsync();

            ViewBag.AllCourses = await _applicationDbContext.Courses.ToListAsync();
            var selectedCourseIds = student.StudentDetails.studentCourses.Select(sc => sc.Cid).ToList();

            var studentDetailsViewModel = new StudentDetailsViewModel
            {
                Id_Student = student.StudentDetails.Id_Student,
                StudentId = student.StudentDetails.StudentId,
                FirstName = student.StudentDetails.firstName_Student,
                LastName = student.StudentDetails.lastName_Student,
                Email = student.StudentDetails.Email_Student,
                Phone = student.StudentDetails.Phone_Student,
                FatherName = student.StudentDetails.FatherName_Student,
                MotherName = student.StudentDetails.MotherName_Student,
                GradeId = student.StudentDetails.GradeId,
                Courses = studentCourses,
                SelectedCourseIds = selectedCourseIds
            };

            return View("Edit", studentDetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(StudentDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
               
                ViewBag.Grades = _applicationDbContext.Grades.ToList();
                return View(model);
            }

            try
            {
                var student = await _applicationDbContext.Students
                    .Include(s => s.StudentDetails)
                    .FirstOrDefaultAsync(s => s.Id == model.Id_Student);

                if (student == null)
                {
                    return NotFound();
                }

                string fullName = $"{model.FirstName} {model.LastName}";

                student.Name_Student = fullName;
                student.Email_Student = model.Email;
                student.Phone_Student = model.Phone;

                
                student.StudentDetails.firstName_Student = model.FirstName;
                student.StudentDetails.lastName_Student = model.LastName;
                student.StudentDetails.Email_Student = model.Email;
                student.StudentDetails.Phone_Student = model.Phone;
                student.StudentDetails.FatherName_Student = model.FatherName;
                student.StudentDetails.MotherName_Student = model.MotherName;
                student.StudentDetails.GradeId = model.GradeId;

                _applicationDbContext.StudentCourses.RemoveRange(_applicationDbContext.StudentCourses.Where(sc => sc.SId == student.StudentDetails.StudentId));

                
                foreach (var courseId in model.SelectedCourseIds)
                {
                    var course = _applicationDbContext.Courses.Find(courseId);
                    if (course != null)
                    {
                       
                        _applicationDbContext.StudentCourses.Add(new StudentCourse { SId = student.StudentDetails.StudentId, Cid = courseId });
                    }
                }

               
                await _applicationDbContext.SaveChangesAsync();

                
                return RedirectToAction("Index", "Student");
            }
            catch (Exception ex)
            {
               
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _applicationDbContext.Students
                .Include(s => s.StudentDetails) 
                .FirstOrDefaultAsync(x => x.Id == id);

            if (student is not null)
            {
                if (student.StudentDetails != null)
                {
                    _applicationDbContext.StudentDetails.Remove(student.StudentDetails);
                }

                _applicationDbContext.Students.Remove(student);
                await _applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Student");
        }




    }
}
