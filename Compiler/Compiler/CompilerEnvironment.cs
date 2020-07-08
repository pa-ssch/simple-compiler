using System;
using Compiler.Compiler;
using Compiler.Instructions;

namespace Compiler
{
    public class CompilerEnvironment
    {
        private readonly InstructionBlock _entryBlock = new InstructionBlock();
        private InstructionBlock _currentBlock;

        public void Compile(System.IO.Stream commands)
        {
            _currentBlock = _entryBlock;

            var lexer = new Lexer(commands);
            var cmdReader = new CommandReader(lexer, this);

            cmdReader.GetCommandList();

            var exEnv = new ExecutionEnvironment(_entryBlock.GetEnumerator());
            exEnv.Execute();
            Output = exEnv.Output;
        }

        public string Output { get; private set; }

        public void AddInstruction(IInstruction instruction) => _currentBlock.Add(instruction);

        public void SetCurrentBlock(InstructionBlock newBlock) => _currentBlock = newBlock;
    }
}
