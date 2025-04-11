using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProject2;

namespace NetProject2
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CourseTeacher> CourseTeachers { get; set; }
    }
}
