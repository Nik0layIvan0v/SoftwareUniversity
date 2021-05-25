using Exercise.Chronometer.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exercise.Chronometer
{
    public class Chronometer : IChronometer
    {
        private List<string> laps;
        private Task task;
        private bool IsStarted;
        private int SavedMinutes;
        private int SavedSeconds;
        private int SavedMilliseconds;


        public Chronometer()
        {
            this.laps = new List<string>();
            this.IsStarted = false;
            this.SavedMinutes = 0;
            this.SavedSeconds = 0;
            this.SavedMilliseconds = 0;
        }

        /// <summary>
        /// Returns the currently recorded time
        /// </summary>
        public string GetTime { get; private set; }

        /// <summary>
        /// Returns all of the currently recorded laps
        /// </summary>
        public List<string> Laps => this.laps.AsReadOnly().ToList();

        /// <summary>
        /// Starts counting time in milliseconds, seconds and minutes.
        /// </summary>
        public void Start()
        {
            IsStarted = true;

            this.task = new Task(() =>
            {
                while (IsStarted)
                {
                    for (int minutesIndex = 0; minutesIndex < 60; minutesIndex++)
                    {
                        this.SavedMinutes = minutesIndex;

                        for (int secondsIndex = 0; secondsIndex < 60; secondsIndex++)
                        {
                            this.SavedSeconds = secondsIndex;

                            for (int milliseconds = 0; milliseconds < 60; milliseconds++)
                            {
                                this.SavedMilliseconds = milliseconds;

                                Thread.Sleep(1);
                                GetTime = $"{SavedMinutes:D2}:{SavedSeconds:D2}:{SavedMilliseconds:D2}";

                                if (!IsStarted)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            });

            this.task.Start();
        }

        /// <summary>
        /// Stops the process of counting time, but the counted time remains.
        /// </summary>
        public void Stop()
        {
            IsStarted = false;
        }

        /// <summary>
        /// Creates a lap at the current time.
        /// </summary>
        /// <returns>Current time</returns>
        public string Lap()
        {
            string lap = Time();

            this.laps.Add(lap);

            return lap;
        }

        /// <summary>
        /// Stops the Chronometer, resets the currently recorded time and deletes all of the currently recoded laps.
        /// </summary>
        public void Reset()
        {
            this.IsStarted = false;
            this.SavedMinutes = 0;
            this.SavedSeconds = 0;
            this.SavedMilliseconds = 0;
            this.laps = new List<string>();
            this.GetTime = $"{SavedMinutes:D2}:{SavedSeconds:D2}:{SavedMilliseconds:D2}";
        }

        /// <summary>
        /// Returns the currently recorded time.
        /// </summary>
        public string Time()
        {
            return this.GetTime;
        }
    }
}
