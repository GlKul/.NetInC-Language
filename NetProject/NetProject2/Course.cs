using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProject2
{
    class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public required string Title { get; set; }
        public int Duration { get; set; }
        public string? Distription { get; set; }
    }
}
