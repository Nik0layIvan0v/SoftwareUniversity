using System;
using System.Threading;

namespace SimpleSnake.MusicPlayer
{
    public class Player : IPlayer
    {
        /*TODO: Replace current player with 
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        player.SoundLocation = "c:\PathToMusic\..music.wav";
        player.Play();*/

        private readonly Thread thread;

        public Player()
        {
            this.thread = new Thread(Melody);
        }

        public void PlayMusic()
        {
            thread.Start();
        }

        private void Melody()
        {
            while (true)
            {
                Console.Beep(659, 125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(523, 125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(784, 125);
                Thread.Sleep(375);
                Console.Beep(392, 125);
                Thread.Sleep(375);
                Console.Beep(523, 125);
                Thread.Sleep(250);
                Console.Beep(392, 125);
                Thread.Sleep(250);
                Console.Beep(330, 125);
                Thread.Sleep(250);
                Console.Beep(440, 125);
                Thread.Sleep(125);
                Console.Beep(494, 125);
                Thread.Sleep(125);
                Console.Beep(466, 125);
                Thread.Sleep(42);
                Console.Beep(440, 125);
                Thread.Sleep(125);
                Console.Beep(392, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(784, 125);
                Thread.Sleep(125);
                Console.Beep(880, 125);
                Thread.Sleep(125);
                Console.Beep(698, 125);
                Console.Beep(784, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(523, 125);
                Thread.Sleep(125);
                Console.Beep(587, 125);
                Console.Beep(494, 125);
                Thread.Sleep(125);
                Console.Beep(523, 125);
                Thread.Sleep(250);
                Console.Beep(392, 125);
                Thread.Sleep(250);
                Console.Beep(330, 125);
                Thread.Sleep(250);
                Console.Beep(440, 125);
                Thread.Sleep(125);
                Console.Beep(494, 125);
                Thread.Sleep(125);
                Console.Beep(466, 125);
                Thread.Sleep(42);
                Console.Beep(440, 125);
                Thread.Sleep(125);
                Console.Beep(392, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(784, 125);
                Thread.Sleep(125);
                Console.Beep(880, 125);
                Thread.Sleep(125);
                Console.Beep(698, 125);
                Console.Beep(784, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(523, 125);
                Thread.Sleep(125);
                Console.Beep(587, 125);
                Console.Beep(494, 125);
                Thread.Sleep(375);
                Console.Beep(784, 125);
                Console.Beep(740, 125);
                Console.Beep(698, 125);
                Thread.Sleep(42);
                Console.Beep(622, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(415, 125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Thread.Sleep(125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Console.Beep(587, 125);
                Thread.Sleep(250);
                Console.Beep(784, 125);
                Console.Beep(740, 125);
                Console.Beep(698, 125);
                Thread.Sleep(42);
                Console.Beep(622, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(698, 125);
                Thread.Sleep(125);
                Console.Beep(698, 125);
                Console.Beep(698, 125);
                Thread.Sleep(625);
                Console.Beep(784, 125);
                Console.Beep(740, 125);
                Console.Beep(698, 125);
                Thread.Sleep(42);
                Console.Beep(622, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(415, 125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Thread.Sleep(125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Console.Beep(587, 125);
                Thread.Sleep(250);
                Console.Beep(622, 125);
                Thread.Sleep(250);
                Console.Beep(587, 125);
                Thread.Sleep(250);
                Console.Beep(523, 125);
                Thread.Sleep(1125);
                Console.Beep(784, 125);
                Console.Beep(740, 125);
                Console.Beep(698, 125);
                Thread.Sleep(42);
                Console.Beep(622, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(415, 125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Thread.Sleep(125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Console.Beep(587, 125);
                Thread.Sleep(250);
                Console.Beep(784, 125);
                Console.Beep(740, 125);
                Console.Beep(698, 125);
                Thread.Sleep(42);
                Console.Beep(622, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(698, 125);
                Thread.Sleep(125);
                Console.Beep(698, 125);
                Console.Beep(698, 125);
                Thread.Sleep(625);
                Console.Beep(784, 125);
                Console.Beep(740, 125);
                Console.Beep(698, 125);
                Thread.Sleep(42);
                Console.Beep(622, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(415, 125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Thread.Sleep(125);
                Console.Beep(440, 125);
                Console.Beep(523, 125);
                Console.Beep(587, 125);
                Thread.Sleep(250);
                Console.Beep(622, 125);
                Thread.Sleep(250);
                Console.Beep(587, 125);
                Thread.Sleep(250);
                Console.Beep(523, 125);
            }
        }

    }
}