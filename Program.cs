using NUnit.Framework;
using NUnit.Framework.Legacy;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Codewars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Kata kata = new Kata();
            kata.ArrayDiffTest();
            kata.DuplicateCountTest();
            kata.PrinterErrorTest();
        }

        public class Kata
        {
            // Your goal in this kata is to implement a difference function,
            // which subtracts one list from another and returns the result.
            // It should remove all values from list a, which are present in list b keeping their order.
            public void ArrayDiffTest()
            {
                ClassicAssert.AreEqual(new int[] { 2 }, Kata.ArrayDiff(new int[] { 1, 2 }, new int[] { 1 }));
                ClassicAssert.AreEqual(new int[] { 2, 2 }, Kata.ArrayDiff(new int[] { 1, 2, 2 }, new int[] { 1 }));
                ClassicAssert.AreEqual(new int[] { 1 }, Kata.ArrayDiff(new int[] { 1, 2, 2 }, new int[] { 2 }));
                ClassicAssert.AreEqual(new int[] { 1, 2, 2 }, Kata.ArrayDiff(new int[] { 1, 2, 2 }, new int[] { }));
                ClassicAssert.AreEqual(new int[] { }, Kata.ArrayDiff(new int[] { }, new int[] { 1, 2 }));
                ClassicAssert.AreEqual(new int[] { 3 }, Kata.ArrayDiff(new int[] { 1, 2, 3 }, new int[] { 1, 2 }));
            }

            private static int[] ArrayDiff(int[] a, int[] b)
            {
                // Your brilliant solution goes here
                // It's possible to pass random tests in about a second ;)
                //int[] c = a.Except(b).ToArray();
                int[] c = a.Where(dupe => !b.Contains(dupe)).ToArray();

                Console.Write("a: ");
                foreach (int i in a)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                Console.Write("b: ");
                foreach (int j in b)
                {
                    Console.Write($"{j} ");
                }
                Console.WriteLine();

                Console.Write("c: ");
                foreach (int k in c)
                {
                    Console.Write($"{k} ");
                }
                Console.WriteLine();

                return c;
            }

            // Return the count of distinct case-insensitive alphabetic characters and
            // numeric digits that occur more than once in the input string.
            // The input string can be assumed to contain only alphabets (both uppercase and lowercase) and numeric digits.
            public void DuplicateCountTest()
            {
                ClassicAssert.AreEqual(0, Kata.DuplicateCount(""));
                ClassicAssert.AreEqual(0, Kata.DuplicateCount("abcde"));
                ClassicAssert.AreEqual(2, Kata.DuplicateCount("aabbcde"));
                ClassicAssert.AreEqual(2, Kata.DuplicateCount("aabBcde"), "should ignore case");
                ClassicAssert.AreEqual(1, Kata.DuplicateCount("Indivisibility"));
                ClassicAssert.AreEqual(2, Kata.DuplicateCount("Indivisibilities"), "characters may not be adjacent");
            }

            private static int DuplicateCount(string str)
            {
                Dictionary<char, int> charDict = new Dictionary<char, int>();

                foreach (char c in str.ToLower())
                {
                    if (charDict.ContainsKey(c))
                    {
                        charDict[c]++;
                    }
                    else
                    {
                        charDict[c] = 1;
                    }
                }

                int dupes = charDict.Where(c => c.Value > 1).Count();

                Console.WriteLine(dupes);

                return dupes;

                /* Alternatively:
      
                return str
                    .ToLower()
                    .GroupBy(c => c)
                    .Count(
                        g => g.Count() > 1);

                */
            }

            // In a factory a printer prints labels for boxes.
            // For one kind of boxes the printer has to use colors which, for the sake of simplicity,
            // are named with letters from a to m.
            // The colors used by the printer are recorded in a control string.
            // For example a "good" control string would be aaabbbbhaijjjm meaning that the printer
            // used three times color a, four times color b, one time color h then one time color a...
            // Sometimes there are problems: lack of colors, technical malfunction and a "bad" control string
            // is produced e.g.aaaxbbbbyyhwawiwjjjwwm with letters not from a to m.
            // You have to write a function printer_error which given a string will return
            // error rate of the printer as a string representing a rational whose numerator
            // is the number of errors and the denominator the length of the control string.
            // Don't reduce this fraction to a simpler expression.
            // The string has a length greater or equal to one and contains only letters from ato z.
            // Examples:
            //  s="aaabbbbhaijjjm"
            //  printer_error(s) => "0/14"
            //  s="aaaxbbbbyyhwawiwjjjwwm"
            //  printer_error(s) => "8/22"
            public void PrinterErrorTest()
            {
                Console.WriteLine("Testing PrinterError");
                string s = "aaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbmmmmmmmmmmmmmmmmmmmxyz";
                string ratio = PrinterError(s);
                Console.WriteLine($"PrinterError ratio: {ratio}");

                ClassicAssert.AreEqual("3/56", ratio);
            }

            public static string PrinterError(string s)
            {
                int errors = s.Where(c => c < 'a' || c > 'm').Count();

                string ratio = errors.ToString() + '/' + s.Length.ToString();

                return ratio;
            }
        }
    }
}
