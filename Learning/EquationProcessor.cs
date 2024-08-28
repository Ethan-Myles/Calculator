
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Learning
{
    public class Processor
    {
        public static void EquationProcessor(string equation)
        {

            string[] words = equation.Split(' ');

            switch (words[0])
            {
                case "mea":
                    Console.WriteLine("\nThe mean is " + ComputeMea(words[1]) + "\n");
                    break;

                case "med":

                    Console.WriteLine("\nThe median is " + ComputeMed(words[1]) + "\n");
                    break;

                case "mode":

                    Console.WriteLine(string.Join(", ", ComputeMode(words[1])) + "\n");
                    break;

                case "ran":

                    Console.WriteLine("\nThe range is " + ComputeRan(words[1]) + "\n");
                    break;

                default:
                    Console.WriteLine("\nUnknown operation, check \"options\" for a list of possible operations\n");
                    break;
            }

            // Might be able to do this a quicker way
            static double[] StringToDoubleArray(string doubleString)
            {

                /*
                ^\\[ - Checks there is an opening bracket at the start

                (?:-?\\d+(?:\\.\\d+)?) - Checks the next element is an integer or decimal with an optional negative sign

                (?:,-?\\d+(?:\\.\\d+)?)* - Checks for a comma and then the next element is an integer or decimal with an optional negative sign zero or more times, so the pattern can continue
                 
                \\]$ - Checks there is a closing sqaure bracket at the start

                ?: - Makes the groups non-capturing, so intermiedate matches aren't uncessarily stored 
                */

                var format = Regex.IsMatch(doubleString, "^\\[(?:-?\\d+(?:\\.\\d+)?)(?:,-?\\d+(?:\\.\\d+)?)*\\]$");

                if (doubleString == "[]")
                {
                    Console.WriteLine("\nPlease provide values as input to the operation");
                    return Array.Empty<double>();
                }

                else if(format == false)
                {
                    Console.WriteLine("\nInput in the incorrect format, check \"options\" for the formats of operations");
                    return Array.Empty<double>();
                }

                string trimmedString = doubleString.Trim('[', ']');

                string[] stringArray = trimmedString.Split(',');

                double[] doubleArray = stringArray.Select(double.Parse).ToArray();

                return doubleArray;
            }

            static double ComputeMea(string doublestring)
            {
                double[] doubleArray = StringToDoubleArray(doublestring);

                if (doubleArray.Length == 0)
                {
                    return double.NaN;
                }

                double itemTotal = 0;

                foreach (var item in doubleArray)
                {
                    itemTotal += item;
                }

                double mean = itemTotal / doubleArray.Length;

                return mean;
            }

            static double ComputeMed(string doublestring)
            {
                double[] doubleArray = StringToDoubleArray(doublestring);

                if (doubleArray.Length == 0)
                {
                    return double.NaN;
                }

                Array.Sort(doubleArray);

                double median;

                //even case
                if (doubleArray.Length % 2 == 0)
                {
                    median = (doubleArray[doubleArray.Length / 2] + doubleArray[doubleArray.Length / 2 - 1]) / 2;
                }

                //odd case
                else
                {
                    median = doubleArray[doubleArray.Length / 2];    
                }

                return median;
            }

            static List<double> ComputeMode(string doublestring)
            {
                double[] doubleArray = StringToDoubleArray(doublestring);

                if (doubleArray.Length == 0)
                {
                    Console.Write("\nThe mode is ");
                    return new List<double> { double.NaN };
                }

                // To find the unique elements of the input array
                HashSet<double> set = doubleArray.ToHashSet();

                double count = 0;
                List<double> modes = new List<double>();

                foreach (var item in set)
                {
                    // There is a single mode
                    if (doubleArray.Count(s => s == item) > count)
                    {
                        count = doubleArray.Count(s => s == item);

                        modes.Clear();
                        modes.Add(item);

                    }

                    // There are multiple modes
                    else if (doubleArray.Count(s => s == item) == count)
                    {
                        modes.Add(item);
                    }
                }

                if (modes.Count > 1)
                {
                    Console.Write("\nThe modes are ");
                }
                else
                {
                    Console.Write("\nThe mode is ");
                }

                return modes;
            }

            static double ComputeRan(string doublestring)
            {

                double[] doubleArray = StringToDoubleArray(doublestring);

                if (doubleArray.Length == 0)
                {
                    return double.NaN;
                }

                double max = doubleArray[0];
                double min = doubleArray[0];

                for (int i = 0; i < doubleArray.Length; i++)
                {
                    if (doubleArray[i] > max)
                    {
                        max = doubleArray[i];
                    }

                    if (doubleArray[i] < min)
                    {
                        min = doubleArray[i];
                    }
                }

                double range = max - min;

                return range;
            }

        }
    }
}