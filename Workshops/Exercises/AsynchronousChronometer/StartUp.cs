using System;
using System.Linq;

namespace Exercise.Chronometer
{
    public class StartUp
    {
        public static void Main()
        {
            Chronometer chronometer = new Chronometer();

            while (true)
            {
                string test = Console.ReadLine();

                switch (test)
                {
                    case "Start":
                        chronometer.Start();
                        break;

                    case "Stop":
                        chronometer.Stop();
                        break;

                    case "Lap":
                        Console.WriteLine(chronometer.Lap());
                        break;

                    case "Time":
                        Console.WriteLine(chronometer.Time());
                        break;

                    case "Laps":
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

                    case "Reset":
                        chronometer.Reset();
                        break;

                    case "Exit":
                        chronometer.Stop();
                        return;
                }
            }
        }
    }
}
