using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Exercise.Chronometer.Contracts;

namespace Exercise.Chronometer
{
    public class ThreadChronometer : IChronometer
    {
        private readonly Thread thread;
        private List<string> laps;
        private bool IsStarted;
        private int SavedMinutes;
        private int SavedSeconds;
        private int SavedMilliseconds;

        public ThreadChronometer()
        {
            this.laps = new List<string>();
            this.thread = new Thread(Action);
        }

        public string GetTime { get; private set; }

        public List<string> Laps => this.laps.AsReadOnly().ToList();

        public void Start()
        {
            this.IsStarted = true;
            this.thread.Start();
        }

        public void Stop()
        {
            this.IsStarted = false;
        }

        public string Lap()
        {
            string lap = this.Time();
            this.laps.Add(lap);
            return lap;
        }

        public void Reset()
        {
            this.IsStarted = false;
            this.laps = new List<string>();
            this.SavedMilliseconds = 0;
            this.SavedMinutes = 0;
            this.SavedSeconds = 0;
            this.GetTime = $"{SavedMinutes:D2}:{SavedSeconds:D2}:{SavedMilliseconds:D2}";
        }

        public string Time()
        {
            return this.GetTime;
        }

        private void Action()
        {
            while (this.IsStarted)
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

                            this.GetTime = $"{SavedMinutes:D2}:{SavedSeconds:D2}:{SavedMilliseconds:D2}";

                            if (!IsStarted)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
