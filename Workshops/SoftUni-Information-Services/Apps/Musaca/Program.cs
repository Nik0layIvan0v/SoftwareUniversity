using System.Threading.Tasks;
using SUS.MvcFramework;

namespace Musaca
{
    public class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync<Startup>();
        }
    }
}
