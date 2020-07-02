using CompilerExtensions;
using System;
using System.Globalization;
using System.Text;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Properties.Resources.Caption);
            Console.WriteLine(Properties.Resources.Seperator);
            Console.WriteLine(Properties.Resources.Grammar);
            Console.WriteLine(Properties.Resources.Seperator);
            Console.WriteLine("Type 'BUILD' to compile");
            Console.WriteLine(Properties.Resources.Seperator);

            do
            {
                // Read Input
                var input = new StringBuilder();
                for (string nextCommand; (nextCommand = Console.ReadLine()) != "BUILD";)
                    input.Append(nextCommand);

                // Compile
                var compiler = new CompilerEnvironment();
                compiler.Compile($"{input}".ToStream());

                // Write Output
                Console.WriteLine(compiler.Output.Replace(";", "\r\n"));

                // Restart?
                Console.WriteLine("Insert and compile other commands? (y/n)");

            } while (Console.ReadLine().StartsWith("y", true, CultureInfo.InvariantCulture));

        }
    }
}
