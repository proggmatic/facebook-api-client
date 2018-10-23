using System;
using System.Threading.Tasks;
using Facebook.Api.Client;

namespace TestConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var facebookClient = new FacebookClient("EAAKEKTQXJzkBAK4ZCenOg5J2YDszZBITE1SfjZBr1r8xiXouhZC18NZCb79ij5aSHLHSgBUgWZBfHPZA1dHsy1v725BePCRkvsJqG8SFMSenDqCtZBaoBy7UBfzlSgGhYjtx9jZAw0qj2Xx3j1XKo6uTNtFw290SLKcGHpZAHXVi9PCgZDZD");

            var meInfo = await facebookClient.GetAsync("me");
        }
    }
}
