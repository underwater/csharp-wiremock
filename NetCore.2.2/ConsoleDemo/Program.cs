using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create a stub http server
            var settings = new WireMockServerSettings
            {
                // Urls = new[] { "http://+:5001" },
                StartAdminInterface = true
            };

            var server = WireMockServer.Start(settings);
            var port = server.Ports[0];
            var url = server.Urls[0];

            Configure(server);

            Console.WriteLine($"Mock Server listening on : {url}");
            Console.WriteLine("Press any key to stop the server");
            Console.ReadLine();
            server.Stop();
        }

        private static void Configure(WireMockServer server)
        {
            //use the c# fluent syntax to add a non-trivial stubbed behaviour
            server.Given(
                Request.Create()
                  .WithPath("/oauth2/access")
                  .UsingGet())

               //.UsingPost()
               //.WithBody("grant_type=password;username=u;password=p"))
               .RespondWith(Response.Create()
                             .WithStatusCode(200)
                             .WithHeader("Content-Type", "application/json")
                             .WithHeader("HELLO_HEADER", "NONESENSE")
                             //.WithBodyFromFile(@"...filename"));
                             .WithBodyAsJson(new { access_token = "AT", refresh_token = "RT" }));
        }
    }
}
