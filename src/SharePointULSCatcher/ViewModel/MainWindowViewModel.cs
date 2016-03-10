using SharePointULSCatcher.Core.Correlations;
using SharePointULSCatcher.Core.FileContent;
using SharePointULSCatcher.Core.File;
using SharePointULSCatcher.Core.Runner;
using System;
using System.Windows;
using System.Windows.Input;

namespace SharePointULSCatcher.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IFileContentDetailsProvider _ContentProvider = new FileContentDetailsProvider();
        private readonly IFileProvider _FileProvider = new DefaultULSFileProvider();
        private readonly ICorrelationRunner _NotepadRunner = new NotepadppCorrelationRunner();
        private readonly ICorrelationRunner _MessageRunner = new UnexpectedMessageCorrelationRunner();

        private String _Text;

        public String Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                OnPropertyChanged();
            }
        }

        public ICommand RunCorrelationCommand
        {
            get { return new SimpleCommand(RunCorrelation); }
        }

        public MainWindowViewModel()
        {
            Text = Clipboard.GetText();
            if (Text.Length > 0)
            {
                Correlation correlation;
                if (CorrelationFactory.TryParse(Text, out correlation))
                    TryRunCorrelation(correlation);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CC0003:Your catch maybe include some Exception", Justification = "Code simplicity")]
        private bool TryRunCorrelation(Correlation correlation)
        {
            try { RunCorrelation(correlation); }
            catch { return false; }
            return true;
        }

        private void RunCorrelation()
        {
            var correlation = CorrelationFactory.Parse(Text);
            RunCorrelation(correlation);
        }

        private void RunCorrelation(Correlation correlation)
        {
            var filePath = _FileProvider.GetFullPathFor(correlation);
            var details = _ContentProvider.GetDetailsForFile(filePath, correlation);

            _NotepadRunner.Run(details);
            _MessageRunner.Run(details);
        }
    }
}