using System;
using System.Linq;
using Exercise.Chronometer.Contracts;

namespace Exercise.Chronometer
{
    public class StartUp
    {
        public static void Main()
        {
            IChronometer chronometer = new ThreadChronometer();

            while (true)
            {
                string test = Console.ReadLine()?.ToLower();

                switch (test)
                {
                    case "start":
                        chronometer.Start();
                        break;

                    case "stop":
                        chronometer.Stop();
                        break;

                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;

                    case "time":
                        Console.WriteLine(chronometer.Time());
                        break;

                    case "laps":
                        if (chronometer.Laps.Count == 0)
                        {
                            Console.WriteLine("Laps: no laps");
                            break;
                        }

                        Console.WriteLine("Laps:");

                        chronometer.Laps
                            .Select(((lapInfo, lapIndex) =>
                        {
                            return $"{lapIndex}. {lapInfo}";

                        }))
                            .ToList()
                            .ForEach(lap => Console.WriteLine(lap));

                        break;

                    case "reset":
                        chronometer.Reset();
                        break;

                    case "exit":
                        chronometer.Stop();
                        return;
                }
            }
        }
    }
}
