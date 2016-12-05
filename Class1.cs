using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NumbersInWordsTest
{
    public class NumberWriterTests
    {
        [TestCase(0, "zero dollars")]
        [TestCase(1, "one dollar")]
        [TestCase(2, "two dollars")]
        [TestCase(12, "twelve dollars")]
        [TestCase(20, "twenty dollars")]
        [TestCase(21, "twenty one dollars")]
        [TestCase(30, "thirty dollars")]
        [TestCase(31, "thirty one dollars")]
        [TestCase(100, "one hundred dollars")]
        [TestCase(100, "one hundred dollars")]
        [TestCase(101, "one hundred and one dollars")]
        [TestCase(131, "one hundred and thirty one dollars")]
        public void Should_write_one_when_number_is_1(decimal input, string expected)
        {
            string actual = WriterNumber.Convert(input);
            Check.That(actual).Equals(expected);
        }
    }

    public class WriterNumber
    {
        static Dictionary<int, string> _tens = new Dictionary<int, string>
        {
            { 0, "zero"},
            { 1, "twenty" },
            { 2, "twenty" },
            { 3, "thirty" },
            { 4, "forty" },
            { 5, "fifty" },
            { 6, "sixty" },
            { 7, "seventy" },
            { 8, "eighty" },
            { 9, "ninety" },
        };


        static Dictionary<int, string> tenFirstMappingDictionary = new Dictionary<int, string>
            {
                {1, "one"},
                {2, "two"},
                {3, "three"},
                {4, "four"},
                {5, "five"},
                {6, "six"},
                {7, "seven"},
                {8, "eight"},
                {9, "nine"},
                {10, "ten"},
                {11, "eleven"},
                {12, "twelve"},
                {13, "thirteen"},
                {14, "fourteen"},
                {15, "fifteen"},
                {16, "sixteen"},
                {17, "seventeen"},
                {18, "eighteen"},
                {19, "nineteen"},
                {20, "twenty"},
            };

        public static string Convert(decimal number)
        {
            string currency = number == 1 ? "dollar" : "dollars";

            int dollars = (int) Decimal.Floor(number);
            string centsChain = GetCentsString(number);

            if (dollars%10 == 0)
            {
                if (dollars / 10 < 10)
                    return $"{_tens[dollars / 10]} {currency}";
                if (dollars / 10 < 100)
                    return $"{tenFirstMappingDictionary[dollars / 100]} hundred {currency}";
            }
            if (dollars < 20)
                return $"{tenFirstMappingDictionary[dollars]} {currency}";    
            if (dollars < 100)
                return $"{_tens[dollars /10]} {tenFirstMappingDictionary[dollars % 10]} {currency}";
            if (dollars < 1000)
                return $"{tenFirstMappingDictionary[dollars/100]} {_tens[dollars%100]} {tenFirstMappingDictionary[dollars%100%10]} {currency}";
            return "";

        }

        private static string GetCentsString(decimal number)
        {
            int cents = (int) (number - Decimal.Floor(number))*100;
            if (cents > 0)
                return $"{ParseCents(cents)} cents";
            return string.Empty;
        }

        private static string ParseCents(int cents)
        {
            throw new NotImplementedException();
        }
    }
}
