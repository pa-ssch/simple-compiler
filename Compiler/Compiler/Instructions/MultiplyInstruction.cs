namespace Compiler.Instructions
{
    class MultiplyInstruction : IInstruction
    {
        public void Execute(ExecutionEnvironment env)
        {
            var lSum = env.PopNumber();
            var rSum = env.PopNumber();
            env.PushNumber(lSum * rSum);
        }
    }
}
