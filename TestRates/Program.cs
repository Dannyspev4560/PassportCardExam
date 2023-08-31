using System;
using System.IO;
using TestRating.App;
using static System.Net.Mime.MediaTypeNames;

namespace TestRating
{
    class Program
    {
        static void Main(string[] args)
        {
            string executableDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // this line removes from the directory the debug path - assuming it runs in debug mode
            string sourceCodeDirectory = Path.GetFullPath(Path.Combine(executableDirectory, @"..\..\..\"));
            string filename = "Logger.txt";

            //should be injected to IoC Container
            var logger = new Logger.Logger(filename);

            logger.Info("Insurance Rating System Starting...");
 

            var engine = new RatingEngine(logger);
            string filePath = "Infrastructure/policy.json";
            engine.Rate(filePath);

            if (engine.Rating > 0)
            {
                logger.Info($"Rating: {engine.Rating}");
            }
            else
            {
                logger.Info("No rating produced.");
            }

        }
    }
}
