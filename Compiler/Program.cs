using System;
using System.Globalization;
using System.IO;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type 'BUILD' to compile");
            Console.WriteLine("One command per Line");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(Properties.Resources.Grammar);
            Console.WriteLine("------------------------------------------------");

            do
            {
                using var stream = new MemoryStream();
                var writer = new StreamWriter(stream);

                for (string nextLine; (nextLine = Console.ReadLine()) != "BUILD";)
                    writer.Write($"{nextLine.Replace(" ", "")};");

                writer.Flush();
                stream.Position = 0;
                var compiler = new CompilerEnvironment();
                compiler.Compile(stream);
                Console.WriteLine(compiler.Output.Replace(";", "\r\n"));

                Console.WriteLine("Insert and compile other commands? (y/n)");
            } while (Console.ReadLine().StartsWith("y", true, CultureInfo.InvariantCulture));

        }
    }
}
