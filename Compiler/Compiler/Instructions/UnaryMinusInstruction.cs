namespace Compiler.Instructions
{
    class UnaryMinusInstruction : IInstruction
    {
        public void Execute(ExecutionEnvironment env) => env.PushNumber(env.PopNumber() * -1);
    }
}
