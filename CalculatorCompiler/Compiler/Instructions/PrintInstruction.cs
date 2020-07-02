namespace CalculatorCompiler.Compiler.Instructions
{
    public class PrintInstruction : IInstruction
    {
        public void Execute(ExecutionEnvironment env) => env.WriteOutput($"{env.PopNumber()} \r\n");
    }
}
