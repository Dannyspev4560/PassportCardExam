using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace TestRating.Logger
{
    public class Logger: ILogger
    {
        private readonly string filename = null;
        public Logger(string filename)
        {
            this.filename = filename;
        }
        public void Info(string message)
        {
            if (File.Exists(filename))
            {
                using var writer = File.AppendText(filename);
                writer.WriteLine(message);
            }
            else
            {
                using var writer = File.CreateText(filename);
                writer.WriteLine(message);
            }
        }
    }
}
