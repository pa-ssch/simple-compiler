using System.Collections.Generic;
using Compiler.Instructions;

namespace Compiler
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
            Output = exEnv.Output;
        }

        public string Output { get; private set; }

        public void AddInstruction(IInstruction instruction) => _instructionStack.Add(instruction);
    }
}
