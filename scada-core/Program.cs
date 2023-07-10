using scada_core.Driver.RealTimeDriver;
using scada_core.Driver.SimulationDriver;
using scada_core.TagProcessing;

namespace scada_core
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ApiClient.ApiClient apiClient = new ApiClient.ApiClient();

            TagProcessor tagProcessingService = new TagProcessor(apiClient);
            WebSocketClient webSocketClient = new WebSocketClient();
            await webSocketClient.ConnectToWebSocket(tagProcessingService);

            tagProcessingService.InitializeTagThreads();
            
            Driver.SimulationDriver.SimulationDriver simulationDriver = new Driver.SimulationDriver.SimulationDriver(apiClient);
            Task simulation = Task.Run(() => simulationDriver.Simulate());
            
            RealTimeDriver realTimeDriver = new RealTimeDriver(apiClient);
            Task realTime = Task.Run(() => realTimeDriver.Simulate());
            
            // WebSocketClient client = new WebSocketClient();
            // await client.Connect();

        }
    }
}