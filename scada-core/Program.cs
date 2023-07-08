using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using scada_core.ApiClient;
using scada_core.TagProcessing;

namespace scada_core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TagProcessingService tagProcessingService = new TagProcessingService();
            Console.WriteLine(tagProcessingService.CreateDriverState(456, 10));
            Console.WriteLine(tagProcessingService.UpdateDriverState(456, 15));
        }
    }
}