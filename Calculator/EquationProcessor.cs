using System.Text.RegularExpressions;

namespace Calculator
{
    public class Processor
    {
        public static void EquationProcessor(string equation)
        {
            var basicFormat = Regex.IsMatch(equation, "^[a-z]+ \\[.*\\]$");

            var variationFormat = Regex.IsMatch(equation, "^[a-z]+ [a-z]+ \\[.*\\]$");

            bool CheckFormats(string typeOfFormat)
            {
                switch (typeOfFormat)
                {
                    case "basic":

                        if (basicFormat == false)
                        {
                            Console.WriteLine("\nInput in the incorrect format, check \"options\" for the formats of operations\n");
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    case "variation":

                        if (variationFormat == false)
                        {
                            Console.WriteLine("\nInput in the incorrect format, check \"options\" for the formats of operations\n");
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    default:
                        Console.WriteLine("\nAn error occured when matching the format of this input\n");
                        return false;
                }
            }

            string[] words = equation.Split(' ');

            switch (words[0])
            {
                case "mea":

                    if (CheckFormats("basic"))
                    {
                        Console.WriteLine("\nThe mean is " + ComputeMea(words[1]) + "\n");
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "med":

                    if (CheckFormats("basic"))
                    {
                        Console.WriteLine("\nThe median is " + ComputeMed(words[1]) + "\n");
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "mode":

                    if (CheckFormats("basic"))
                    {
                        Console.WriteLine(string.Join(", ", ComputeMode(words[1])) + "\n");
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "ran":

                    if (CheckFormats("basic"))
                    {
                        Console.WriteLine("\nThe range is " + ComputeRan(words[1]) + "\n");
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "sd":

                    if (CheckFormats("variation"))
                    {
                        Console.WriteLine("\nThe standard deviation is " + ComputeSdAndVar(words[1], words[2], true) + "\n");
                        break;
                    }
                    else
                    {
                        break;
                    }

                case "var":

                    if (CheckFormats("variation"))
                    {
                        Console.WriteLine("\nThe variance is " + ComputeSdAndVar(words[1], words[2], false) + "\n");
                        break;
                    }
                    else
                    {
                        break;
                    }

                default:
                    Console.WriteLine("\nUnknown operation, check \"options\" for a list of possible operations\n");
                    break;
            }

            static double ComputeSquareRoot(double num)
            {
                double guess = num / 2;
                double previousGuess;

                do
                {
                    previousGuess = guess;
                    guess = (guess + num / guess) / 2;

                }
                while ((guess > previousGuess ? guess - previousGuess : previousGuess - guess) > 0.0001);

                return guess;

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

                var inputFormat = Regex.IsMatch(doubleString, "^\\[(?:-?\\d+(?:\\.\\d+)?)(?:,-?\\d+(?:\\.\\d+)?)*\\]$");

                if (doubleString == "[]")
                {
                    Console.WriteLine("\nPlease provide values as input to the operation");
                    return Array.Empty<double>();
                }

                else if (inputFormat == false)
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

            static double ComputeSdAndVar(string sampleOrPopulation, string doublestring, bool sd)
            {

                if (sampleOrPopulation != "s" && sampleOrPopulation != "p")
                {
                    Console.WriteLine("\nInput in the incorrect format, check \"options\" for the formats of operations");
                }

                double[] doubleArray = StringToDoubleArray(doublestring);

                if (doubleArray.Length == 0)
                {
                    return double.NaN;
                }

                double mean = ComputeMea(doublestring);

                double numerator = 0;

                double standardDeviation = 0;

                foreach (double value in doubleArray)
                {
                    numerator += (value - mean) * (value - mean);
                }

                double variance = double.NaN;

                if (sampleOrPopulation == "s")
                {
                    double denom = doubleArray.Length - 1;

                    variance = (numerator / (doubleArray.Length - 1));
                }
                else if (sampleOrPopulation == "p")
                {
                    variance = (numerator / doubleArray.Length);
                }

                if (sd)
                {
                    standardDeviation = ComputeSquareRoot(variance);
                    return standardDeviation;
                }
                else
                {
                    return variance;

                }
            }
        }

    }
}