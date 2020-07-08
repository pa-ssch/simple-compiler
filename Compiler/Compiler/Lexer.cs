using System.IO;
using System;
using System.Text;
using Compiler.Exceptions;

namespace Compiler
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

        /// <summary>
        /// Consumes an expected Token
        /// </summary>
        /// <param name="type">Type of the expected Token</param>
        /// <exception cref="UnexpectedTokenException">Thrown when the next Token is not the expected Token</exception>
        public void Expect(Token.EType type)
        {
            if (_nextToken.Type != type)
                throw new UnexpectedTokenException($"{_nextToken.Type}", $"{type}");
            Advance();
        }

        /// <summary>
        /// Consumes current Token and sets the new next Token
        /// </summary>
        public void Advance()
        {
            _nextToken = Peek() switch
            {
                char c when c == '=' => GetToken(Token.EType.ASSIGN, c),
                char c when Char.IsLetter(c) => GetIdentifierToken(),
                char c when Char.IsNumber(c) => GetIntegerToken(),
                char c when c == '+' => GetToken(Token.EType.PLUS, c),
                char c when c == '-' => GetToken(Token.EType.UNARYMINUS, c),
                char c when c == '(' => GetToken(Token.EType.LPAREN, c),
                char c when c == ')' => GetToken(Token.EType.RPAREN, c),
                char c when c == '*' => GetToken(Token.EType.MUL, c),
                char c when c == ';' => GetToken(Token.EType.SEMICOL, c),
                char c when c == ':' => GetToken(Token.EType.COLON, c),
                char c when c == '?' => GetToken(Token.EType.QUESTIONMARK, c),
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
            while (Char.IsNumber(Peek()))
                intValue.Append(Read());

            return new Token(Token.EType.INTEGER, Int32.Parse($"{intValue}"));
        }

        private Token GetIdentifierToken()
        {
            if(Peek() == 'P')
            {
                foreach (var c in "PRINT")
                    Expect(c);

                return new Token(Token.EType.PRINT);
            }

            StringBuilder identifier = new StringBuilder();
            while (Char.IsLetter(Peek()))
                identifier.Append(Read());

            return new Token(Token.EType.IDENTIFIER, $"{identifier}");
        }

        public Token LookAheadToken() => _nextToken;

        /// <summary>
        /// Consumes an expected Char
        /// </summary>
        /// <param name="c">Char to Expect</param>
        /// <exception cref="UnexpectedCharacterException">Thrown when the next Char is not the expected Char</exception>
        private void Expect(char c)
        {
            var nextChar = Read();
            if (nextChar != c)
                throw new UnexpectedCharacterException(nextChar, c);
        }

        /// <summary>
        /// Peeks and returns the next 'not-whitespace' Character
        /// </summary>
        /// <returns>The next 'not-whitespace' Character</returns>
        private char Peek()
        {
            while (char.IsWhiteSpace((char)_streamReader.Peek()))
                _streamReader.Read();

            return (char)_streamReader.Peek();
        }

        /// <summary>
        /// Reads and returns the next 'not-whitespace' Character
        /// </summary>
        /// <returns>The next 'not-whitespace' Character</returns>
        private char Read()
        {
            while (char.IsWhiteSpace((char)_streamReader.Peek()))
                _streamReader.Read();

            return (char)_streamReader.Read();
        }
    }
}