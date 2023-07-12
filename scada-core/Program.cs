using scada_core.Driver;
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
            IBatchMediator mediator = new BatchMediator(apiClient);
            TagProcessor tagProcessingService = new TagProcessor(apiClient);
            WebSocketClient webSocketClient = new WebSocketClient();
            webSocketClient.ConnectToWebSocket(tagProcessingService);

            tagProcessingService.InitializeTagThreads();

            DriverService service = new DriverService(apiClient);
            
            Driver.SimulationDriver.SimulationDriver simulationDriver = new Driver.SimulationDriver.SimulationDriver(mediator, service);
            Task simulation = Task.Run(() => simulationDriver.Simulate());
            
            RealTimeDriver realTimeDriver = new RealTimeDriver(mediator, service);
            Task realTime = Task.Run(() => realTimeDriver.Simulate());
            
            // WebSocketClient client = new WebSocketClient();
            // await client.Connect();

        }
    }
}