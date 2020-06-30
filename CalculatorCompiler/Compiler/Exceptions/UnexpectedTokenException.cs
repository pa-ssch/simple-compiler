namespace CalculatorCompiler.Compiler.Exceptions
{
    public class UnexpectedTokenException : System.Exception
    {
        public UnexpectedTokenException(string actualTokenType, string expectedToken)
            : base($"Expected Token {expectedToken}, but there was a freakin {actualTokenType}") { }
    }
}
