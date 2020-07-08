using Compiler.Compiler;

namespace Compiler.Instructions
{
    class ConditionalJumpInstruction : IInstruction
    {
        private readonly InstructionBlock _trueBlock;
        private readonly InstructionBlock _falseBlock;

        public ConditionalJumpInstruction(InstructionBlock trueBlock, InstructionBlock falseBlock)
        {
            _trueBlock = trueBlock;
            _falseBlock = falseBlock;
        }

        public void Execute(ExecutionEnvironment env)
        {
            if (env.PopNumber() == 1)
                env.SetEnumerator(_trueBlock.GetEnumerator());
            else
                env.SetEnumerator(_falseBlock.GetEnumerator());
        }
    }
}
