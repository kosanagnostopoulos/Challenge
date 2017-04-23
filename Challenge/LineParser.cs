using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class LineParser : ILineParser
    {
        private const int NUMBER_OF_OCCURENCES_COMMA_CHAR = 1;
        private const int FIRST_NAME = 0;
        private const int SECOND_NAME = 1;

        public Tuple<string, string> Parse(string line)
        {
            VerifyLineHasContent(line);
            VerifyLineContainsTheCorrectOccurencesOfCommaChar(line);
            return new Tuple<string, string>(line.Split(',')[FIRST_NAME], line.Split(',')[SECOND_NAME]);
        }

        private static void VerifyLineHasContent(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new ArgumentException(nameof(line));
            }
        }

        private void VerifyLineContainsTheCorrectOccurencesOfCommaChar(string line)
        {
            int numOccurences = OccurencesOfCommaChar(line);
            if (numOccurences < NUMBER_OF_OCCURENCES_COMMA_CHAR)
            {
                throw new ArgumentException($"Missing comma character in line: {line}");
            }
            if (numOccurences > NUMBER_OF_OCCURENCES_COMMA_CHAR)
            {
                throw new ArgumentException($"More comma characters in line: {line}");
            }
        }

        private int OccurencesOfCommaChar(string line)
        {
            return line.ToCharArray().Count(s => s == ',');
        }
    }
}
