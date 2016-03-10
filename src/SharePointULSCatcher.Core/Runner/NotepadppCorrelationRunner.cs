using SharePointULSCatcher.Core.FileContent;
using System;
using System.Configuration;
using System.Diagnostics;

namespace SharePointULSCatcher.Core.Runner
{
    public class NotepadppCorrelationRunner : ICorrelationRunner
    {
        private static readonly String NPP_PATH; 

        static NotepadppCorrelationRunner()
        {
            NPP_PATH = ConfigurationManager.AppSettings["NppPath"];
        }

        public void Run(FileContentDetails fileDetails)
        {
            var arguments = String.Format("\"{1}\" -n{2}", NPP_PATH, fileDetails.File, fileDetails.GuidFirstOccurrenceLine + 1);
            var startInfo = new ProcessStartInfo(NPP_PATH, arguments);

            Process.Start(startInfo);
        }
    }
}