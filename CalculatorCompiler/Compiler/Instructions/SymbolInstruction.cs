namespace CalculatorCompiler.Compiler.Instructions
{
    class SymbolInstruction : IInstruction
    {
        private readonly string _symbolName;

        public SymbolInstruction(string symbolName)
        {
            _symbolName = symbolName;
        }

        public void Execute(ExecutionEnvironment env)
        {
            var intValue = env.Symbols.Get(_symbolName).IntegerValue;
            env.PushNumber(intValue);
        }
    }
}
