
using Learning;

namespace HelloWorld
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the calculator\n\nType \"options\" or an equation\n");

            while (true)
            {
                string? equation = Console.ReadLine();

                if (equation == null)
                {
                    Console.WriteLine("The equation given is null");
                    // To tell the switch statement the null case has been recognised
                    // Not sure this is correct now
                    continue;
                }

                switch (equation)
                {
                    case "options":

                        string options = GetOptions();
                        Console.WriteLine(options + "\n");
                        break;

                    // returns from the main method which exits the code
                    case "exit":
                        return;

                    default:
                        Processor.EquationProcessor(equation);
                        break;

                }
            }
        }

        static string GetOptions()
        {
            try
            {
                using StreamReader reader = new("options.txt");
                string text = reader.ReadToEnd();
                return text;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while reading the options file.", ex);
            }

        }
    }
}