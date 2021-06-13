using SUS.HTTP;
using SUS.MvcFramework;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    public class Program
    {
        public static async Task Main()
        {
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "http://localhost/"); //Add :{Port} from .CreateHostAsync<Startup>()

            //Port number is optional!
            await Host.CreateHostAsync<Startup>(HttpConstants.DefaultPortNumber);
        }
    }
}
