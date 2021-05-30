using System.Diagnostics;
using System.Threading.Tasks;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp
{
    public class Program
    {
        public static async Task Main()
        {
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "http://localhost/");

            //Port number is optional!
            await Host.CreateHostAsync<Startup>(HttpConstants.DefaultPortNumber); 
        }
    }
}
