using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProject2;

namespace NetProject2
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }

        public ICollection<CourseTeacher> CourseTeachers { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }

    public class CourseStudent
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

    public class CourseTeacher
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
