using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Timers;

public class Program
{

    public static string name;
    public static double lifetime, frequency;
    private static System.Timers.Timer aTimer;



    public static void Main(string[] args)
    {
        if (args.Length == 3)
        {
            name = args[0];
            lifetime = Convert.ToDouble(args[1]);
            frequency = Convert.ToDouble(args[2]);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Enter the name");
            name = Console.ReadLine();
            Console.WriteLine("\n");
            Console.WriteLine("Enter the life time (minutes)");
            while (!double.TryParse(Console.ReadLine(), out lifetime))
            {
                Console.WriteLine("only numbers are allowed");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Enter the time of checking frequency (minutes)");
            while (!double.TryParse(Console.ReadLine(), out frequency))
            {
                Console.WriteLine("only numbers are allowed");
            }
            Console.WriteLine("\n");
        }

        Console.WriteLine("Enter ,,Q,, to quit");
        SetTimer(frequency);
        OnTimedEvent(null, null);

        while (true)
        {
            char ch = Console.ReadKey().KeyChar;
            switch (Char.ToLower(ch))
            {
                case 'q':
                    return;
                default:
                    Console.WriteLine("\nPlease Enter Valid Choice");
                    break;
            }
        }

    }

    private static void SetTimer(double frequency)
    {
        aTimer = new System.Timers.Timer(frequency * 60000);
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        string processName = name;
        Process[] processes = Process.GetProcessesByName(processName);
        foreach (Process process in processes)
        {
            TimeSpan runningTime = DateTime.Now - process.StartTime;
            if (runningTime.TotalMinutes < lifetime)
            {
                Console.WriteLine("Process " + name + " is running " + runningTime.TotalMinutes + " minute(s)");
            }
            else
            {
                process.Kill();
                Console.WriteLine("Process " + name + " with ID " + process.Id + " was killed.");

            }
        }
    }
}
