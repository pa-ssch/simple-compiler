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
            if ((char)_streamReader.Peek() == ';')
                _streamReader.Read();

            _nextToken = (char)_streamReader.Peek() switch
            {
                '=' => GetAssignToken(),
                char c when Char.IsLetter(c) => GetIdentifierToken(),
                char c when Char.IsNumber(c) => GetIntegerToken(),
                '+' => GetPlusToken(),
                _ => GetEofToken(),
            };
        }

        private Token GetEofToken() => new Token(Token.EType.EOF);

        private Token GetPlusToken()
        {
            Expect('+');
            return new Token(Token.EType.PLUS);
        }

        private Token GetAssignToken()
        {
            Expect('=');
            return new Token(Token.EType.ASSIGN);
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