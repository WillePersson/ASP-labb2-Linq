using LinqBuild.Data;
using LinqBuild.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LinqBuild.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Operations()
        {
            return View(); 
        }

        public async Task<IActionResult> StudentsTeachers()
        {
            var students = await _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .ThenInclude(c => c.TeacherCourses)
                .ThenInclude(tc => tc.Teacher)
                .ToListAsync();

            return View(students);
        }

        public async Task<IActionResult> SelectCourseForStudentsAndTeachers()
        {
            var courses = await _context.Courses.ToListAsync();
            return View("SelectCourse", courses);  
        }

        public async Task<IActionResult> ShowStudentsAndTeachersInCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .Include(c => c.TeacherCourses)
                .ThenInclude(tc => tc.Teacher)
                .FirstOrDefaultAsync(m => m.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        public IActionResult SelectCourseForEditing()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            // Get the list of teacher IDs associated with the course
            var teacherIds = await _context.TeacherCourses
                .Where(tc => tc.CourseId == id)
                .Select(tc => tc.TeacherId)
                .ToListAsync();

            // Get the list of all teachers
            var teachers = await _context.Teachers
                .Select(t => new SelectListItem
                {
                    Value = t.TeacherId.ToString(),
                    Text = t.Name,
                    Selected = teacherIds.Contains(t.TeacherId)
                })
                .ToListAsync();

            // Populate the view model
            var viewModel = new CourseEditViewModel
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Teachers = teachers
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(CourseEditViewModel viewModel)
        {
            if (viewModel.SelectedTeacherIds == null)
            {
                viewModel.SelectedTeacherIds = new int[0]; 
            }
            if (ModelState.IsValid)
            {
                var course = await _context.Courses.FindAsync(viewModel.CourseId);
                if (course == null)
                {
                    return NotFound();
                }

                course.CourseName = viewModel.CourseName;

                var teacherIds = viewModel.SelectedTeacherIds ?? new int[0];
                var existingTeacherIds = await _context.TeacherCourses
                    .Where(tc => tc.CourseId == viewModel.CourseId)
                    .Select(tc => tc.TeacherId)
                    .ToArrayAsync();

                var teachersToAdd = teacherIds.Except(existingTeacherIds);
                var teachersToRemove = existingTeacherIds.Except(teacherIds);

                foreach (var teacherId in teachersToAdd)
                {
                    _context.Add(new TeacherCourse { CourseId = viewModel.CourseId, TeacherId = teacherId });
                }

                foreach (var teacherId in teachersToRemove)
                {
                    var teacherCourse = await _context.TeacherCourses
                        .FirstOrDefaultAsync(tc => tc.CourseId == viewModel.CourseId && tc.TeacherId == teacherId);

                    if (teacherCourse != null)
                    {
                        _context.Remove(teacherCourse);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}
