namespace CalculatorCompiler.Compiler
{
    class Token
    {
        public enum EType
        {
            PRINT,
            IDENTIFIER,
            INTEGER,
            PLUS,
            EOF,
            ASSIGN
        }

        public EType Type;

        public Token(EType type)
        {
            Type = type;
        }

        public Token(EType type, string stringValue)
        {
            Type = type;
            StringValue = stringValue;
        }

        public Token(EType type, int intValue)
        {
            Type = type;
            IntegerValue = intValue;
        }

        public readonly int IntegerValue;
        public readonly string StringValue;
    }
}
