using SharePointULSCatcher.Core.Correlations;
using System;

namespace SharePointULSCatcher.Core.File
{
    public interface IFileProvider
    {
        String GetFullPathFor(Correlation correlation);
    }
}
