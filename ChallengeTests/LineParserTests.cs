using Challenge;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeTests
{
    [TestClass]
    public class LineParserTest
    {
        private const string LINE_WITHOUT_COMMA = "MPLA_MPLAGKOUX_GKOUX";
        private const string LINE_WITH_MORE_THAN_ONE_COMMA = "MPLA_MPLA,GKOUX,_GKOUX";
        private const string FIRST_NAME = "MPLA_MPLA";
        private const string SECOND_NAME = "GKOUX_GKOUX";
        private const string CORRECT_LINE = FIRST_NAME + "," + SECOND_NAME;

        private readonly LineParser _parser = new LineParser();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineParserShouldNotAcceptNullAsInput()
        {
            _parser.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineParserShouldNotAcceptEmptyLinesAsInput()
        {
            _parser.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineMustContainOneComma()
        {
            _parser.Parse(LINE_WITHOUT_COMMA);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineShouldNOTContainMoreThanOneComma()
        {
            _parser.Parse(LINE_WITH_MORE_THAN_ONE_COMMA);
        }

        [TestMethod]
        public void ShouldSplitTheLineIntoTwoNames()
        {
            Assert.AreEqual(new Tuple<string, string>(FIRST_NAME, SECOND_NAME), _parser.Parse(CORRECT_LINE));
        }
    }
}
