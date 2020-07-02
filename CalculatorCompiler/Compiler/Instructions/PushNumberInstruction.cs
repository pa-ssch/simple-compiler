namespace CalculatorCompiler.Compiler.Instructions
{
    class PushNumberInstruction : IInstruction
    {
        private readonly int _number;
        public PushNumberInstruction(int number)
        {
            _number = number;
        }

        public void Execute(ExecutionEnvironment env) => env.PushNumber(_number);
    }
}
