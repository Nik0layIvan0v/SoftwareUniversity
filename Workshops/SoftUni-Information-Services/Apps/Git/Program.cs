namespace Git
{
    using System.Threading.Tasks;

    using SUS.MvcFramework;

    public class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync<Startup>();
        }
    }
}
