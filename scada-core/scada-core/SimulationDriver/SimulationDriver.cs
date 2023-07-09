using Newtonsoft.Json.Linq;
using scada_core_6.ApiClient;
using scada_core.SimulationDriver.Signals;

namespace scada_core.SimulationDriver;

public class SimulationDriver
{
    private List<SignalGenerator> generators;
    private List<Task> tasks;
    string apiUrl = "https://example.com/api";

    public SimulationDriver()
    {
        generators = new List<SignalGenerator>();
        tasks = new List<Task>();
        
        generators.Add(new TrigGenerator(Math.Sin));
        generators.Add(new TrigGenerator(Math.Sin));
        generators.Add(new TrigGenerator(Math.Sin));
        
        generators.Add(new TrigGenerator(Math.Cos));
        generators.Add(new TrigGenerator(Math.Cos));
        generators.Add(new TrigGenerator(Math.Cos));
        
        generators.Add(new RampGenerator());
        generators.Add(new RampGenerator());
        generators.Add(new RampGenerator());
    }

    public void Simulate()
    {
        for (int i = 0; i < generators.Count(); i++)
        {
            tasks.Add(Task.Run(() => Generate(generators[i])));
        }

        Task.WaitAll(tasks.ToArray());
    }

    private async Task Generate(SignalGenerator generator)
    {
        ApiClient client = new ApiClient();
        while (true)
        {
            double value = generator.GetAndNext();
            var payload = new
            {
                number = value
            };

            try
            {
                string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                // response?
                JToken token = await client.MakeApiRequest(apiUrl, HttpMethod.Post, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Value: {value} sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to send value: {value}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the random number: {ex.Message}");
            }

            Thread.Sleep(1000);
        }
    }
}