namespace LinqBuild.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public ICollection<TeacherCourse> TeacherCourses { get; set; } 
        public ICollection<Class> Classes { get; set; }
    }
}
