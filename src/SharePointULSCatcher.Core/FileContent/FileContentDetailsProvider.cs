using SharePointULSCatcher.Core.Correlations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharePointULSCatcher.Core.FileContent
{
    public class FileContentDetailsProvider : IFileContentDetailsProvider
    {
        public FileContentDetails GetDetailsForFile(String file, Correlation correlation)
        {
            var firstUnexpectedMessages = new List<String>();
            var catchedUnexpectedMessages = false;

            var line = String.Empty;
            var currLine = 0;
            var lines = new List<Int32>();

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs))
                while ((line = reader.ReadLine()) != null)
                {
                    HasGuidReferenceInLineAndAccUpdated(correlation, line, lines, currLine);

                    var inUnexpectedMessages = !catchedUnexpectedMessages && lines.Any() && IsUnexpectedLevel(line);

                    if (inUnexpectedMessages)
                    {
                        currLine--;

                        do
                        {
                            firstUnexpectedMessages.Add(line.Substring(150));
                            currLine++;
                            HasGuidReferenceInLineAndAccUpdated(correlation, line, lines, currLine);

                        } while ((line = reader.ReadLine()) != null && IsUnexpectedLevel(line));

                        HasGuidReferenceInLineAndAccUpdated(correlation, line, lines, currLine);
                        catchedUnexpectedMessages = true;
                    }

                    currLine++;
                }

            var unexpectedMessages = FormatUnexpectedMessages(firstUnexpectedMessages);

            return new FileContentDetails(file, lines, unexpectedMessages);
        }

        private static bool IsUnexpectedLevel(string line)
        {
            return line
                .Substring(139, 10)
                .Equals("Unexpected");
        }

        private static bool HasGuidReferenceInLineAndAccUpdated(Correlation correlation, String line, List<Int32> accumulator, Int32 idxCurrent)
        {
            var hasReference = line.Contains(correlation.Guid.ToString());

            if (hasReference)
                accumulator.Add(idxCurrent);

            return hasReference;
        }

        private static String FormatUnexpectedMessages(List<String> messages)
        {
            const int c_SuffixLength = 41;
            const int c_PreffixLength = 3;
            
            if (!messages.Any())
                return String.Empty;

            var sb = new StringBuilder();
            
            var first = messages.First();
            
            first = first.Substring(0, first.Length - c_SuffixLength);
            first = ReplaceLineSeparators(first);

            sb.Append(first);

            var query = messages
                .Skip(1)
                .Take(messages.Count - 2)
                .ToList();

            foreach (var item in query)
            {
                var length = item.Length - c_SuffixLength - c_PreffixLength;
                var formated = item.Substring(c_PreffixLength, length);

                formated = ReplaceLineSeparators(formated);
                sb.Append(formated);
            }

            var last = messages.Last()
                .Substring(c_PreffixLength);

            last = ReplaceLineSeparators(last);

            sb.Append(last);

            return sb.ToString();
        }

        private static String ReplaceLineSeparators(String text)
        {
            const string c_LineSeparatorX = "     ";
            const string c_LineSeparatorM = "    ";
            const string c_NewLine = "\n";

            return text
                .Replace(c_LineSeparatorX, c_NewLine)
                .Replace(c_LineSeparatorM, c_NewLine);
        }
    }
}