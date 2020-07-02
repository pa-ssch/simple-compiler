using Compiler.Exceptions;
using Compiler.Instructions;

namespace Compiler
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
                GetAssignStatement();
            else if (_lexer.LookAheadToken().Type == Token.EType.PRINT)
                GetPrintStatement();
            else
                throw new UnexpectedTokenException($"{_lexer.LookAheadToken().Type}", $"{Token.EType.IDENTIFIER}, {Token.EType.PRINT} or {Token.EType.EOF}");

            _lexer.Expect(Token.EType.SEMICOL);
        }

        private void GetAssignStatement()
        {
            var token = _lexer.LookAheadToken();
            _lexer.Expect(Token.EType.IDENTIFIER);
            _lexer.Expect(Token.EType.ASSIGN);
            GetSumExpression();

            _compilerEnvironment.AddInstruction(new AssignInstruction(token.StringValue));
        }

        private void GetPrintStatement()
        {
            _lexer.Expect(Token.EType.PRINT);
            GetSumExpression();
            _compilerEnvironment.AddInstruction(new PrintInstruction());
        }

        private void GetSumExpression()
        {
            GetProductExpression();

            while (_lexer.LookAheadToken().Type == Token.EType.PLUS)
            {
                _lexer.Expect(Token.EType.PLUS);
                GetProductExpression();
                _compilerEnvironment.AddInstruction(new AddInstruction());
            }
        }

        private void GetProductExpression()
        {
            GetUnaryExpression();

            while (_lexer.LookAheadToken().Type == Token.EType.MUL)
            {
                _lexer.Expect(Token.EType.MUL);
                GetUnaryExpression();
                _compilerEnvironment.AddInstruction(new MultiplyInstruction());
            }
        }

        private void GetUnaryExpression()
        {
            var token = _lexer.LookAheadToken();

            if (token.Type == Token.EType.UNARYMINUS)
                _lexer.Advance();

            GetAtomicValue();

            if (token.Type == Token.EType.UNARYMINUS)
                _compilerEnvironment.AddInstruction(new UnaryMinusInstruction());
        }

        private void GetAtomicValue()
        {
            var token = _lexer.LookAheadToken();

            if (token.Type == Token.EType.INTEGER)
            {
                _lexer.Advance();
                _compilerEnvironment.AddInstruction(new PushNumberInstruction(token.IntegerValue));
            }
            else if (token.Type == Token.EType.IDENTIFIER)
            {
                _lexer.Advance();
                _compilerEnvironment.AddInstruction(new SymbolInstruction(token.StringValue));
            }
            else if (token.Type == Token.EType.LPAREN)
            {
                _lexer.Expect(Token.EType.LPAREN);
                GetSumExpression();
                _lexer.Expect(Token.EType.RPAREN);
            }
            else
            {
                throw new UnexpectedTokenException($"{token.Type}", $"{Token.EType.LPAREN}, {Token.EType.INTEGER} or {Token.EType.IDENTIFIER}");
            }
        }
    }
}