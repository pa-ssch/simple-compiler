namespace CalculatorCompiler.Compiler.Instructions
{
    class AddInstruction : IInstruction
    {
        public void Execute(ExecutionEnvironment env)
        {
            var lSum = env.PopNumber();
            var rSum = env.PopNumber();
            env.PushNumber(lSum + rSum);
        }
    }
}
