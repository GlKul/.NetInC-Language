using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class ProgrammLab01
{
    static void Main()
    {
        Console.WriteLine("Основной поток начал работу!");

        Thread thread1 = new Thread(() => PrintNumbers(0, 10));
        Thread thread2 = new Thread(() => PrintNumbers(20, 30));

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Основной поток закончил работу работу!");
    }

    static void PrintNumbers(int NumberStart, int NumberEnd)
    {
        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал работу");

        for (int i = NumberStart; i <= NumberEnd; i++)
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {i}");
            Thread.Sleep(100); // Имитация работы
        }

        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} завершил работу");

    }
}