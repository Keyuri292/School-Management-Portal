using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POC_DataAccess.Data;
using POC_Models.Models;
using POC_Models.Models.ViewModel;

namespace POC_Implementation_EFCore.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CourseController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            var courses = _applicationDbContext.Courses.ToList();
            return View(courses);
        }
        public IActionResult Search(string searchString)
        {

            searchString = searchString ?? "";
            var courses = _applicationDbContext.Courses.FromSqlInterpolated($"EXEC SearchCourses {searchString}").ToList();
            return View("Index", courses);
        }
        public ActionResult Add()
        {
            var viewModel = new CourseViewModel
            {
                AvailableTeachers = _applicationDbContext.Teachers.ToList(),
                AvailableStudents = _applicationDbContext.StudentDetails.ToList(),
                SelectedTeacherIds = new List<int>(),
                SelectedStudentIds = new List<int>()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CourseViewModel viewModel, List<int> selectedTeacherIds)
        {
            
                var course = new Courses
                {
                    CourseName = viewModel.Name_Course,
                    CourseDescription = viewModel.Description_Course,
                    teacherCourses = new List<TeacherCourses>(),
                    studentCourses = new List<StudentCourse>()
                };

                foreach (var teacherId in selectedTeacherIds)
                {
                    var teacher = _applicationDbContext.Teachers.Find(teacherId);
                    if (teacher != null)
                    {
                        course.teacherCourses.Add(new TeacherCourses { Teacher = teacher });
                    }
                }
            foreach (var studentId in viewModel.SelectedStudentIds)
            {
                var student = _applicationDbContext.StudentDetails.Find(studentId);
                if (student != null)
                {
                    course.studentCourses.Add(new StudentCourse { StudentDetails = student });
                }
            }
            _applicationDbContext.Courses.Add(course);
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            
        }
        
        public ActionResult Edit(int id)
        {
            var course = _applicationDbContext.Courses
                .Include(c => c.teacherCourses)
                    .ThenInclude(tc => tc.Teacher)
                .Include(c => c.studentCourses)
                    .ThenInclude(sc => sc.StudentDetails)
                .SingleOrDefault(c => c.Id_Courses == id);

            if (course == null)
            {
                return View("Index");
            }

            var viewModel = new CourseViewModel
            {
                Id_Course = course.Id_Courses,
                Name_Course = course.CourseName,
                Description_Course = course.CourseDescription,
                AvailableTeachers = _applicationDbContext.Teachers.ToList(),
                SelectedTeacherIds = course.teacherCourses.Select(ct => ct.Teacher.Id_Teacher).ToList(),
                AvailableStudents = _applicationDbContext.StudentDetails.ToList(),
                SelectedStudentIds = course.studentCourses.Select(sc => sc.StudentDetails.Id_Student).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel viewModel, List<int> selectedTeacherIds, List<int> selectedStudentIds)
        {
            var course = _applicationDbContext.Courses.Include(c => c.teacherCourses).Include(c => c.studentCourses).SingleOrDefault(c => c.Id_Courses == viewModel.Id_Course);

            if (course == null)
            {
                return View("Index");
            }

            course.CourseName = viewModel.Name_Course;
            course.CourseDescription = viewModel.Description_Course;

            // Update teachers
            course.teacherCourses.Clear();
            foreach (var teacherId in selectedTeacherIds)
            {
                var teacher = _applicationDbContext.Teachers.Find(teacherId);
                if (teacher != null)
                {
                    course.teacherCourses.Add(new TeacherCourses { Teacher = teacher });
                }
            }

            // Update students
            course.studentCourses.Clear();
            foreach (var studentId in selectedStudentIds)
            {
                var student = _applicationDbContext.StudentDetails.Find(studentId);
                if (student != null)
                {
                    course.studentCourses.Add(new StudentCourse { StudentDetails = student });
                }
            }

            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var course = _applicationDbContext.Courses
                .Include(c => c.teacherCourses)
                    .ThenInclude(tc => tc.Teacher)
                .Include(c => c.studentCourses) 
                    .ThenInclude(sc => sc.StudentDetails) 
                .SingleOrDefault(c => c.Id_Courses == id);

            if (course == null)
            {
                return View("Index");
            }

            return View(course);
        }


        public ActionResult Delete(int id)
        {
            var course = _applicationDbContext.Courses.SingleOrDefault(c => c.Id_Courses == id);
            if (course == null)
            {
                return View("Index");
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var course = _applicationDbContext.Courses.SingleOrDefault(c => c.Id_Courses == id);
            if (course == null)
            {
                return View("Index");
            }

            _applicationDbContext.Courses.Remove(course);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
   