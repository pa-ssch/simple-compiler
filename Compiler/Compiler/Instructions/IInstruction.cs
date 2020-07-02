namespace Compiler.Instructions
{
    public interface IInstruction
    {
        public void Execute(ExecutionEnvironment env);
    }
}
