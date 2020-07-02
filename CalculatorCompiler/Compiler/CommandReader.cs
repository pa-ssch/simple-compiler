using CalculatorCompiler.Compiler.Exceptions;
using CalculatorCompiler.Compiler.Instructions;

namespace CalculatorCompiler.Compiler
{
    internal class CommandReader
    {
        private readonly Lexer _lexer;
        private readonly CompilerEnvironment _compilerEnvironment;

        public CommandReader(Lexer lexer, CompilerEnvironment compilerEnvironment)
        {
            _lexer = lexer;
            _compilerEnvironment = compilerEnvironment;
        }

        public void GetCommandList()
        {
            while (_lexer.LookAheadToken().Type != Token.EType.EOF)
                GetCommand();
        }

        private void GetCommand()
        {
            if (_lexer.LookAheadToken().Type == Token.EType.IDENTIFIER)
                ReadAssignStatement();
            else if (_lexer.LookAheadToken().Type == Token.EType.PRINT)
                GetPrintStatement();
            else
                throw new UnexpectedTokenException($"{Token.EType.IDENTIFIER}, {Token.EType.PRINT} or {Token.EType.EOF}", $"{_lexer.LookAheadToken().Type}");
        }

        private void GetPrintStatement()
        {
            _lexer.Expect(Token.EType.PRINT);
            GetExpression();
            _compilerEnvironment.AddInstruction(new PrintInstruction());
        }

        private void GetExpression()
        {
            GetAtomicValue();

            while (_lexer.LookAheadToken().Type == Token.EType.PLUS)
            {
                _lexer.Expect(Token.EType.PLUS);
                GetExpression();
                _compilerEnvironment.AddInstruction(new AddInstruction());
            }
        }

        private void GetAtomicValue()
        {
            var token = _lexer.LookAheadToken();
            _lexer.Advance();

            if (token.Type == Token.EType.INTEGER)
                _compilerEnvironment.AddInstruction(new PushNumberInstruction(token.IntegerValue));
            else if (token.Type == Token.EType.IDENTIFIER)
                _compilerEnvironment.AddInstruction(new SymbolInstruction(token.StringValue));
            else
                throw new UnexpectedTokenException($"{Token.EType.INTEGER} or {Token.EType.IDENTIFIER}", $"{token.Type}");
        }

        private void ReadAssignStatement()
        {
            var token = _lexer.LookAheadToken();
            _lexer.Expect(Token.EType.IDENTIFIER);
            _lexer.Expect(Token.EType.ASSIGN);
            GetExpression();

            _compilerEnvironment.AddInstruction(new AssignInstruction(token.StringValue));
        }
    }
}