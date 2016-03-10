using System;

namespace SharePointULSCatcher.Core.Correlations
{
    public class Correlation
    {
        public Guid Guid { get; private set; }
        public DateTime Date { get; private set; }

        public Correlation(Guid guid, DateTime date)
        {
            Guid = guid;
            Date = date;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Guid, Date);
        }
    }
}
