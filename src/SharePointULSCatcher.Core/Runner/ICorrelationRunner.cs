using SharePointULSCatcher.Core.FileContent;

namespace SharePointULSCatcher.Core.Runner
{
    public interface ICorrelationRunner
    {
        void Run(FileContentDetails fileDetails);
    }
}
