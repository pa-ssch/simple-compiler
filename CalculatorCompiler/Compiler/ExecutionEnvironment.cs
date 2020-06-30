using CalculatorCompiler.Compiler.Instructions;
using System.Collections.Generic;

namespace CalculatorCompiler.Compiler
{
    public class ExecutionEnvironment
    {
        private readonly List<IInstruction> _instructions;
        private readonly Stack<int> _numberStack;

        public ExecutionEnvironment(List<IInstruction> instructionStack)
        {
            Symbols = new SymbolTable();
            _instructions = instructionStack;
            _numberStack = new Stack<int>();
        }

        public int PopNumber() => _numberStack.Pop();

        public void Execute() => _instructions.ForEach(i => i.Execute(this));

        public void AddInstruction(IInstruction instruction) => _instructions.Add(instruction);

        public void PushNumber(int integerValue) => _numberStack.Push(integerValue);

        public void WriteOutput(string value) => Output += value;

        public SymbolTable Symbols { get; }

        public string Output { get; private set; }
    }
}
