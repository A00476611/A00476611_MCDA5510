using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAssignment
{
    internal class AssignmentLogger: IDisposable
    {
        private StreamWriter logWriter;
        public string filePath;
        public AssignmentLogger(string logDir) {
            filePath = logDir+"\\log.txt";
            logWriter = new StreamWriter(filePath);
        }

        public void Log(string message)
        {
            logWriter.WriteLine(message);
        }

        public void LogSeparator()
        {
            Log("------------------------------------------------------------------");
        }
        
        public void LogInvalidRow(int rowNumber, string reason)
        {
            Log($"INVALID ROW AT: {rowNumber} \tReason: {reason}");
        }

        public void LogStartOfDir(string filePath)
        {
            Log($"Reading {filePath}");
            LogSeparator();
        }

        public void Dispose()
        {
            logWriter.Dispose();
        }
    }
}
