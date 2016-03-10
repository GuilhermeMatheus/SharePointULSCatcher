using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePointULSCatcher.Core.FileContent
{
    public class FileContentDetails
    {
        private List<Int32> _Lines;
        public String File { get; private set; }
        public String AdditionalInfo { get; private set; }
        public Int32 GuidFirstOccurrenceLine { get; private set; }
        public IReadOnlyList<Int32> Lines
        {
            get { return _Lines.AsReadOnly(); }
        }

        public FileContentDetails(String file, List<Int32> lines, String additionalInfo)
        {
            _Lines = lines;
            File = file;
            GuidFirstOccurrenceLine = lines.First();
            AdditionalInfo = additionalInfo;
        }
    }
}
