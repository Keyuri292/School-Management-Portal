using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POC_DataAccess.Data;
using POC_Implementation_EFCore.Models;
using POC_Models.Models;
using POC_Models.Models.ViewModel;

namespace POC_Implementation_EFCore.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TeacherController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public IActionResult Index(string sortColumn, string sortOrder)
        {
            sortColumn ??= "None";
            sortOrder ??= "None";

            if (sortColumn == "None" || sortOrder == "None")
            {
                var teachers = _applicationDbContext.Teachers.ToList();
                return View(teachers);
            }
            else
            {
                var sortedTeachers = _applicationDbContext.Teachers.FromSqlRaw("EXEC SortTeachers @SortColumn={0}, @SortOrder={1}", sortColumn, sortOrder).ToList();

                ViewData["SortColumn"] = sortColumn;
                ViewData["SortOrder"] = sortOrder;

                return View(sortedTeachers);
            }

        }
        public ActionResult Add()
        {
            var viewModel = new TeacherViewModel
            {
                AvailableCourses = _applicationDbContext.Courses.ToList(), 
                SelectedCourseIds = new List<int>()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(TeacherViewModel viewModel, List<int> selectedCourseIds)
        {
            
                var teacher = new Teacher
                {
                    Name_Teacher = viewModel.Name_Teacher,
                    Email_Teacher = viewModel.Email_Teacher,
                    Phone_Teacher = viewModel.Phone_Teacher,
                    teacherCourses = new List<TeacherCourses>()
                };

                foreach (var courseId in selectedCourseIds)
                {
                    var course = _applicationDbContext.Courses.Find(courseId);
                    if (course != null)
                    {
                        teacher.teacherCourses.Add(new TeacherCourses { Courses = course });
                    }
                }

                _applicationDbContext.Teachers.Add(teacher);
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            
        }

        

        public ActionResult Edit(int id)
        {
            var teacher = _applicationDbContext.Teachers.Include(t => t.teacherCourses).SingleOrDefault(t => t.Id_Teacher == id);
            if (teacher == null)
            {
                return View(Index);
            }

            var viewModel = new TeacherViewModel
            {
                Id_Teacher = teacher.Id_Teacher,
                Name_Teacher = teacher.Name_Teacher,
                Email_Teacher = teacher.Email_Teacher,
                Phone_Teacher = teacher.Phone_Teacher,
                AvailableCourses = _applicationDbContext.Courses.ToList(),
               
                SelectedCourseIds = teacher.teacherCourses.Select(tc => tc.Courses.Id_Courses).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(TeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = _applicationDbContext.Teachers.Include(t => t.teacherCourses).Single(t => t.Id_Teacher == viewModel.Id_Teacher);

                teacher.Name_Teacher = viewModel.Name_Teacher;
                teacher.Email_Teacher = viewModel.Email_Teacher;
                teacher.Phone_Teacher = viewModel.Phone_Teacher;

                teacher.teacherCourses.Clear();

                foreach (var courseId in viewModel.SelectedCourseIds)
                {
                    var course = _applicationDbContext.Courses.Find(courseId);
                    if (course != null)
                    {
                        teacher.teacherCourses.Add(new TeacherCourses { Courses = course });
                    }
                }

                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.AvailableCourses = _applicationDbContext.Courses.ToList();
            return View(viewModel);
        }

       

        public ActionResult Details(int id)
        {
            var teacher = _applicationDbContext.Teachers
                .Include(t => t.teacherCourses) 
                    .ThenInclude(tc => tc.Courses) 
                    .ThenInclude(c => c.studentCourses) 
                        .ThenInclude(sc => sc.StudentDetails) 
                .SingleOrDefault(t => t.Id_Teacher == id);

            if (teacher == null)
            {
                return View("Index");
            }

            return View(teacher);
        }

        public ActionResult Delete(int id)
        {
            var teacher = _applicationDbContext.Teachers.SingleOrDefault(t => t.Id_Teacher == id);
            if (teacher == null)
            {
                return View(Index);
            }

            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var teacher = _applicationDbContext.Teachers.SingleOrDefault(t => t.Id_Teacher == id);
            if (teacher == null)
            {
                return View(Index);
            }

            _applicationDbContext.Teachers.Remove(teacher);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
