using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProject2
{
    class Context : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи Course-Teacher
            modelBuilder.Entity<CourseTeacher>()
                .HasKey(ct => new { ct.CourseId, ct.TeacherId });

            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTeachers)
                .HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Teacher)
                .WithMany(t => t.CourseTeachers)
                .HasForeignKey(ct => ct.TeacherId);

            // Настройка связи Course-Student
            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.CourseId, cs.StudentId });

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);
        }

        public void SeedData()
        {
            if (!Students.Any() && !Courses.Any() && !Teachers.Any())
            {
                // Создание и добавление сущностей
                var s1 = new Student { Name = "Vasya" };
                var s2 = new Student { Name = "Ivan" };
                Students.AddRange(s1, s2);

                var course1 = new Course { Title = "Математика", Duration = 144, Description = "IMIT1&3" };
                var course2 = new Course { Title = "Физика", Duration = 144, Description = "IMIT2" };
                Courses.AddRange(course1, course2);

                var t1 = new Teacher { Name = "Alex" };
                var t2 = new Teacher { Name = "Sergey" };
                Teachers.AddRange(t1, t2);

                SaveChanges(); // Получаем ID для всех сущностей

                // Теперь создаем связи с корректными ID
                CourseTeachers.AddRange(
                    new CourseTeacher { CourseId = course1.Id, TeacherId = t1.Id },
                    new CourseTeacher { CourseId = course2.Id, TeacherId = t2.Id }
                );

                CourseStudents.AddRange(
                    new CourseStudent { CourseId = course1.Id, StudentId = s1.Id },
                    new CourseStudent { CourseId = course1.Id, StudentId = s2.Id },
                    new CourseStudent { CourseId = course2.Id, StudentId = s1.Id }
                );

                SaveChanges();
            }
        }
    }
}
