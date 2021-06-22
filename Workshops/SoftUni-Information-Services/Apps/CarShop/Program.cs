using SUS.MvcFramework;

using System.Threading.Tasks;

namespace CarShop
{
    public static class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync<Startup>();
        }
    }
}
