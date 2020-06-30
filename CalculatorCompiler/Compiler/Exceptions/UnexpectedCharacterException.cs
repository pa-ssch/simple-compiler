namespace CalculatorCompiler.Compiler.Exceptions
{
    public class UnexpectedCharacterException : System.Exception
    {
        public UnexpectedCharacterException(char actualChar, char expectedChar)
            : base($"Expected Character '{expectedChar}', but there was a freakin '{actualChar}'") { }
    }
}
