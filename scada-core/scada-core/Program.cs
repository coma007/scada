
using scada_core.TagProcessing;

namespace scada_core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // qwe
            TagProcessor tagProcessingService = new TagProcessor();
            tagProcessingService.InitializeTagThreads();
        }
    }
}