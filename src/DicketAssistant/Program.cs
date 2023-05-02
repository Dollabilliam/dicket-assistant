using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var client = new HttpClient();
        var url = "https://fahrkarten.bahn.de/privatkunde/start/start.post?scope=bahnzk&zkaktion=dticket";

        while (true)
        {
            try
            {
                Console.WriteLine("getting {0}", url);
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = response.RequestMessage.RequestUri.ToString();

                if (content.Contains("error"))
                {
                    Console.WriteLine("Error found in response, {0}, continuing loop...", content);
                }
                else
                {
                    Console.WriteLine("No error found in response, opening link in Chrome...");
                    Process.Start("chrome.exe", response.RequestMessage.RequestUri.ToString());
                    break;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            var random = new Random();
            var randomNumber = random.Next(15, 61);
            await Task.Delay(TimeSpan.FromSeconds(randomNumber));
        }
    }
}