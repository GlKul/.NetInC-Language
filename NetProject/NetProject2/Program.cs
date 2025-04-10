using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetProject2;
using System;

using (Context db = new())
{
    var courses = db.Courses.ToList();

    foreach (var course in courses)
    {
        Console.WriteLine($"{course.Title} {course.Duration} - {course.Distription}");
    }
}