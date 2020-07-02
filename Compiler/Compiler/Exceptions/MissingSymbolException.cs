namespace Compiler.Exceptions
{
    public class MissingSymbolException : System.Exception
    {
        public MissingSymbolException(string symbolName)
            : base($"The Symbol {symbolName} is not known.") { }
    }
}
