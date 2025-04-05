using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProject
{
    class ProgrammLab1_5
    {
        static async Task Main(string[] args)
        {
            List<Grade> grades = new List<Grade>
        {
            new Grade { StudentName = "Вася", Subject = "Математика", Score = 90 },
            new Grade { StudentName = "Вася", Subject = "Физика", Score = 85 },
            new Grade { StudentName = "Петя", Subject = "Математика", Score = 75 },
            new Grade { StudentName = "Петя", Subject = "Физика", Score = 80 },
            new Grade { StudentName = "Коля", Subject = "Математика", Score = 95 },
            new Grade { StudentName = "Коля", Subject = "Физика", Score = 90 }
        };

            // Список студентов
            List<string> students = new List<string> { "Вася", "Петя", "Коля" };

            // Замер времени выполнения
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Асинхронно вычисляем средний балл для каждого студента
            var calculationTasks = students.Select(student => CalculateAverageScoreAsync(grades, student));

            var results = await Task.WhenAll(calculationTasks);

            // Выводим результаты
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"Для студента {students[i]} средняя оценка: {results[i]}");
            }

            watch.Stop();
            Console.WriteLine($"Все вычисления завершены. Время выполнения: {watch.ElapsedMilliseconds} мс");
        }

        public static async Task<double> CalculateAverageScoreAsync(List<Grade> grades, string studentName)
        {
            // Имитация асинхронной операции (например, запроса к базе данных)
            await Task.Delay(100); // Задержка для имитации долгой операции

            var studentGrades = grades.Where(g => g.StudentName == studentName).ToList();

            if (studentGrades.Count == 0)
                return 0;

            double totalScore = studentGrades.Sum(g => g.Score);
            return totalScore / studentGrades.Count;
        }
    }
}
