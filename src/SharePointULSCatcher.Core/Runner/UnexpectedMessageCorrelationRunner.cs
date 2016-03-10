using SharePointULSCatcher.Core.FileContent;
using System;
using System.Diagnostics;
using System.IO;

namespace SharePointULSCatcher.Core.Runner
{
    public class UnexpectedMessageCorrelationRunner : ICorrelationRunner
    {
        private static readonly String NPP_PATH;

        static UnexpectedMessageCorrelationRunner()
        {
            NPP_PATH = System.Configuration.ConfigurationManager.AppSettings["NppPath"];
        }

        public void Run(FileContentDetails fileDetails)
        {
            var info = fileDetails.AdditionalInfo;
            var hasAdditionalInfo = !String.IsNullOrEmpty(info);

            if (hasAdditionalInfo)
            {
                var tempFile = Path.GetTempFileName();
                tempFile = tempFile.Substring(0, tempFile.Length - 4) + Guid.NewGuid().ToString() + ".txt";

                System.IO.File.WriteAllText(tempFile, info);
                
                var processInfo = new ProcessStartInfo(NPP_PATH, tempFile);
                Process.Start(processInfo);
            }
        }
    }
}