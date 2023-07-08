
using scada_core_6.ApiClient;
using scada_core.TagProcessing;

namespace scada_core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApiClient apiClient = new ApiClient();
            TagProcessor tagProcessingService = new TagProcessor(apiClient);
            tagProcessingService.InitializeTagThreads();
        }
    }
}