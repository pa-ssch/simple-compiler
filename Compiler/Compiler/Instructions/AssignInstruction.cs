namespace Compiler.Instructions
{
    class AssignInstruction : IInstruction
    {
        private readonly string _symbolName;

        public AssignInstruction(string symbolName)
        {
            _symbolName = symbolName;
        }
        public void Execute(ExecutionEnvironment env)
        {
            Symbol symbol = env.Symbols.Create(_symbolName);
            symbol.IntegerValue = env.PopNumber();
        }
    }
}
