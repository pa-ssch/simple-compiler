using System.Collections.Generic;
using CalculatorCompiler.Compiler.Instructions;

namespace CalculatorCompiler.Compiler
{
    public class CompilerEnvironment
    {
        private readonly List<IInstruction> _instructionStack = new List<IInstruction>();

        public void Compile(System.IO.Stream commands)
        {
            var lexer = new Lexer(commands);
            var cmdReader = new CommandReader(lexer, this);

            cmdReader.GetCommandList();

            var exEnv = new ExecutionEnvironment(_instructionStack);
            exEnv.Execute();
            System.Console.WriteLine(exEnv.Output);
        }

        public void AddInstruction(IInstruction instruction) => _instructionStack.Add(instruction);
    }
}
