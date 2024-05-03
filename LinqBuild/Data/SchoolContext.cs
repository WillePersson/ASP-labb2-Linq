using System;
using System.Collections.Generic;
using System.Linq;
using LinqBuild.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqBuild.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        private List<string> GetStudentNames()
        {
            return new List<string>
            {
                "James Smith", "Maria Garcia", "Robert Johnson", "Sophia Martinez", "Michael Brown",
                "Linda Taylor", "William Jones", "Barbara Wilson", "Elizabeth Moore", "David Lee",
                "Oscar Howard", "Edith Singleton", "Jame Spears", "Victor Horne", "Dale Osborn",
                "Dominique Hendricks", "Rodrigo Chen", "Latoya Barry", "Joaquin Soto", "Raymond Donaldson",
                "Shari Valenzuela", "Maude Le", "Claudine Pruitt", "Fannie Ferrell", "Imogene Gilmore",
                "Roberto Park", "Prince Gallegos", "Colby Bowen", "Jody Wilkinson", "Graciela Mendez"
            };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<TeacherCourse>()
                .HasKey(tc => new { tc.TeacherId, tc.CourseId });

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Teacher)
                .WithMany(t => t.TeacherCourses)
                .HasForeignKey(tc => tc.TeacherId);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourses)
                .HasForeignKey(tc => tc.CourseId);

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { TeacherId = 1, Name = "Mr. Smith" },
                new Teacher { TeacherId = 2, Name = "Ms. Johnson" },
                new Teacher { TeacherId = 3, Name = "Mr. Clark" },
                new Teacher { TeacherId = 4, Name = "Ms. Davis" },
                new Teacher { TeacherId = 5, Name = "Mr. Brown" },
                new Teacher { TeacherId = 6, Name = "Ms. Miller" }
            );

            modelBuilder.Entity<Class>().HasData(
                new Class { ClassId = 1, ClassName = "Class 1", TeacherId = 1 },
                new Class { ClassId = 2, ClassName = "Class 2", TeacherId = 2 },
                new Class { ClassId = 3, ClassName = "Class 3", TeacherId = 3 },
                new Class { ClassId = 4, ClassName = "Class 4", TeacherId = 4 },
                new Class { ClassId = 5, ClassName = "Class 5", TeacherId = 5 },
                new Class { ClassId = 6, ClassName = "Class 6", TeacherId = 6 }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "Backend" },
                new Course { CourseId = 2, CourseName = "Frontend" },
                new Course { CourseId = 3, CourseName = "OOP" }
            );

            var teacherCourses = new List<TeacherCourse>();
            var random = new Random();
            for (int i = 1; i <= 6; i++)
            {
                // Randomly select the number of courses each teacher will have
                int numCourses = random.Next(1, 3);

                // Randomly select course IDs for the teacher
                var courseIds = Enumerable.Range(1, 3).OrderBy(x => random.Next()).Take(numCourses);

                foreach (var courseId in courseIds)
                {
                    teacherCourses.Add(new TeacherCourse { TeacherId = i, CourseId = courseId });
                }
            }
            modelBuilder.Entity<TeacherCourse>().HasData(teacherCourses);

            var students = new List<Student>();
            var studentCourses = new List<StudentCourse>();
            var studentNames = GetStudentNames();
            for (int i = 0; i < studentNames.Count; i++)
            {
                var newStudent = new Student
                {
                    StudentId = i + 1,
                    Name = studentNames[i],
                    ClassId = ((i + 1) % 6) + 1
                };
                students.Add(newStudent);

                // Randomly select the number of courses each student will take
                int numCourses = random.Next(1, 4);

                // Randomly select course IDs for the student
                var courseIds = Enumerable.Range(1, 3).OrderBy(x => random.Next()).Take(numCourses);

                foreach (var courseId in courseIds)
                {
                    studentCourses.Add(new StudentCourse { StudentId = i + 1, CourseId = courseId });
                }
            }
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<StudentCourse>().HasData(studentCourses);
        }
    }
}