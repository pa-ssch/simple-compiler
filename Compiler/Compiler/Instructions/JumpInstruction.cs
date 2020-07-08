using Compiler.Compiler;

namespace Compiler.Instructions
{
    class JumpInstruction : IInstruction
    {
        private readonly InstructionBlock _targetBlock;

        public JumpInstruction(InstructionBlock targetBlock)
        {
            _targetBlock = targetBlock;
        }

        public void Execute(ExecutionEnvironment env) => env.SetEnumerator(_targetBlock.GetEnumerator());
    }
}
