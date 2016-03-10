using SharePointULSCatcher.Core.Correlations;
using System;

namespace SharePointULSCatcher.Core.FileContent
{
    public interface IFileContentDetailsProvider
    {
        FileContentDetails GetDetailsForFile(String file, Correlation correlation);
    }
}
