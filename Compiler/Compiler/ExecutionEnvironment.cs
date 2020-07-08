using Compiler.Compiler;
using Compiler.Instructions;
using System.Collections.Generic;

namespace Compiler
{
    public class ExecutionEnvironment
    {
        private List<IInstruction>.Enumerator _instructionEnumerator;
        private readonly Stack<int> _numberStack;


        public ExecutionEnvironment(List<IInstruction>.Enumerator instructionEnumerator)
        {
            Symbols = new SymbolTable();
            _instructionEnumerator = instructionEnumerator;
            _numberStack = new Stack<int>();
        }

        public int PopNumber() => _numberStack.Pop();

        public void Execute()
        {
            while (_instructionEnumerator.MoveNext())
                _instructionEnumerator.Current.Execute(this);
        }

        public void SetEnumerator(List<IInstruction>.Enumerator instructionEnumerator) => _instructionEnumerator = instructionEnumerator;

        public void PushNumber(int integerValue) => _numberStack.Push(integerValue);

        public void WriteOutput(string value) => Output += value;

        public SymbolTable Symbols { get; }

        public string Output { get; private set; }
    }
}
