using System.Security.Claims;

namespace LinqBuild.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }  
        public Class Class { get; set; }  
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
