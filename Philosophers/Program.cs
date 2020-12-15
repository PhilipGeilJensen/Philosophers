using System;
using System.Collections.Generic;
using System.Threading;

namespace Philosophers
{
    class Program
    {
        static object _lock = new object();
        // false = not in use - true = in use
        static bool[] forks = new bool[5] { false, false, false, false, false };
        static void Main(string[] args)
        {
            Thread p1 = new Thread(CheckAvailability);
            p1.Name = "Philosopher 1";
            List<int> philo1 = new List<int>() { 0, 1 };
            Thread p2 = new Thread(CheckAvailability);
            p2.Name = "Philosopher 2";
            List<int> philo2 = new List<int>() { 1, 2 };
            Thread p3 = new Thread(CheckAvailability);
            p3.Name = "Philosopher 3";
            List<int> philo3 = new List<int>() { 2, 3 };
            Thread p4 = new Thread(CheckAvailability);
            p4.Name = "Philosopher 4";
            List<int> philo4 = new List<int>() { 3, 4 };
            Thread p5 = new Thread(CheckAvailability);
            p5.Name = "Philosopher 5";
            List<int> philo5 = new List<int>() { 4, 0 };
            p1.Start(philo1);
            p2.Start(philo2);
            p3.Start(philo3);
            p4.Start(philo4);
            p5.Start(philo5);

        }

        static void CheckAvailability(object indexes)
        {
            List<int> i = indexes as List<int>;
            while (true)
            {
                Monitor.Enter(forks);
                if (forks[i[0]] == true || forks[i[1]] == true)
                {
                    Console.WriteLine("{0} is now waiting", Thread.CurrentThread.Name);
                    Monitor.Wait(forks);
                }
                forks[i[0]] = true;
                forks[i[1]] = true;
                Monitor.Pulse(forks);
                Console.WriteLine("{0} is now eating", Thread.CurrentThread.Name);
                Thread.Sleep(2000);

                Monitor.Exit(forks);

                forks[i[0]] = false;
                forks[i[1]] = false;
                Console.WriteLine("{0} is now thinking", Thread.CurrentThread.Name);
                Thread.Sleep(5000);
            }
        }
    }
}
