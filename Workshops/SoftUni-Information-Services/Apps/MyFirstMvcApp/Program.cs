using System.Diagnostics;
using System.Threading.Tasks;
using SUS.MvcFramework;

namespace MyFirstMvcApp
{
    public class Program
    {
        public static async Task Main()
        {
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "http://localhost/");

            await Host.CreateHostAsync<Startup>();
        }
    }
}
