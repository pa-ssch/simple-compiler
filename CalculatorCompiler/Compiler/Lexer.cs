using System.IO;
using System;
using System.Text;
using CalculatorCompiler.Compiler.Exceptions;

namespace CalculatorCompiler.Compiler
{
    internal class Lexer
    {
        private readonly StreamReader _streamReader;
        private Token _nextToken;

        public Lexer(Stream stream)
        {
            _streamReader = new StreamReader(stream);
            Advance();
        }

        public void Expect(Token.EType type)
        {
            if (_nextToken.Type != type)
                throw new UnexpectedTokenException($"{_nextToken.Type}", $"{type}");
            Advance();
        }

        public void Advance()
        {
            while ((char)_streamReader.Peek() == ';' || Char.IsWhiteSpace((char)_streamReader.Peek()))
                _streamReader.Read();

            _nextToken = (char)_streamReader.Peek() switch
            {
                char c when c == '=' => GetToken(Token.EType.ASSIGN, c),
                char c when Char.IsLetter(c) => GetIdentifierToken(),
                char c when Char.IsNumber(c) => GetIntegerToken(),
                char c when c == '+' => GetToken(Token.EType.PLUS, c),
                char c when c == '-' => GetToken(Token.EType.UNARYMINUS, c),
                char c when c == '(' => GetToken(Token.EType.LPAREN, c),
                char c when c == ')' => GetToken(Token.EType.RPAREN, c),
                char c when c == '*' => GetToken(Token.EType.MUL, c),
                _ => GetToken(Token.EType.EOF)
            };
        }

        private Token GetToken(Token.EType tokenType, char? expectedChar = null)
        {
            if (expectedChar.HasValue)
                Expect(expectedChar.Value);

            return new Token(tokenType);
        }

        private Token GetIntegerToken()
        {
            StringBuilder intValue = new StringBuilder();
            while (Char.IsNumber((char)_streamReader.Peek()))
                intValue.Append((char)_streamReader.Read());

            return new Token(Token.EType.INTEGER, Int32.Parse($"{intValue}"));
        }

        private Token GetIdentifierToken()
        {
            if((char)_streamReader.Peek() == 'P')
            {
                foreach (var c in "PRINT")
                    Expect(c);

                return new Token(Token.EType.PRINT);
            }

            StringBuilder identifier = new StringBuilder();
            while (Char.IsLetter((char)_streamReader.Peek()))
                identifier.Append((char)_streamReader.Read());

            return new Token(Token.EType.IDENTIFIER, $"{identifier}");
        }

        public Token LookAheadToken() => _nextToken;

        private void Expect(char c)
        {
            var nextChar = (char)_streamReader.Read();
            if (nextChar != c)
                throw new UnexpectedCharacterException(nextChar, c);
        }
    }
}