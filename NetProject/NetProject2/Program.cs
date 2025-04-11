using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetProject2;
using System;

class Programm
{
    public static void Main()
    {

        using (Context db = new())
        {
            db.SeedData();

            // Eager loading (жадная загрузка)
            var coursesWithTeachersAndStudents = db.Courses
                .Include(c => c.CourseTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
                .ToList();

            foreach (var course in coursesWithTeachersAndStudents)
            {
                Console.WriteLine($"{course.Title} {course.Duration} - {course.Description}");
            }

            // Explicit loading (явная загрузка)
            var courseExplicit = db.Courses.First();
            db.Entry(courseExplicit)
                .Collection(c => c.CourseTeachers)
                .Query()
                .Include(ct => ct.Teacher)
                .Load();

            db.Entry(courseExplicit)
                .Collection(c => c.CourseStudents)
                .Query()
                .Include(cs => cs.Student)
                .Load();

            var lazyCourse = db.Courses.First();
            var teachers = lazyCourse.CourseTeachers.Select(ct => ct.Teacher); // Загрузится при первом обращении
            var students = lazyCourse.CourseStudents.Select(cs => cs.Student); // Загрузится при первом обращении
        }
    }
}