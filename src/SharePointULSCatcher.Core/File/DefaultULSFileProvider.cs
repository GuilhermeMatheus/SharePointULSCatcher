using SharePointULSCatcher.Core.Correlations;
using System;
using System.IO;
using System.Linq;

namespace SharePointULSCatcher.Core.File
{
    public class DefaultULSFileProvider : IFileProvider
    {
        private readonly String _searchPatternFormat = "*{0:yyyyMMdd}*.log";
        private readonly String _logPath;

        public DefaultULSFileProvider()
            : this(@"C:\Program Files\Common Files\microsoft shared\Web Server Extensions\15\LOGS\") { }

        public DefaultULSFileProvider(String logPath)
        {
            _logPath = logPath;
        }

        public string GetFullPathFor(Correlation correlation)
        {
            var searchPattern = String.Format(_searchPatternFormat, correlation.Date);
            
            var files = Directory.GetFiles(_logPath, searchPattern, SearchOption.TopDirectoryOnly);

            if (!files.Any())
                throw new FileNotFoundException();

            var correlationTime = correlation.Date.Hour * 100 + correlation.Date.Minute;

            var query = from item in files
                        let time = GetTimePart(item)
                        where time < correlationTime
                        select item;
            
            var file = query.LastOrDefault();
            return file;
        }

        private static int GetTimePart(String file)
        {
            //"MACHINE-20160203-0856.log"
            var timePart = file.Substring(file.Length - 8, 4);
            return Int32.Parse(timePart);
        }
    }
}
