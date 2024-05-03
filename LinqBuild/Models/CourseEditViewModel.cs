using Microsoft.AspNetCore.Mvc.Rendering;

namespace LinqBuild.Models
{
    public class CourseEditViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<SelectListItem>? Teachers { get; set; }
        public int[] SelectedTeacherIds { get; set; }
    }
}
