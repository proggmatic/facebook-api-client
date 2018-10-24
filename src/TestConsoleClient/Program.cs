using System;
using System.Threading.Tasks;
using Facebook;

namespace TestConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var facebookClient = new FacebookApi("shit");

            try
            {
                var meInfo = await facebookClient.GetAsync("me");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
