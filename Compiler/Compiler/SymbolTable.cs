using Compiler.Exceptions;
using System.Collections.Generic;

namespace Compiler
{
    public class SymbolTable
    {
        private readonly Dictionary<string, Symbol> _symbols = new Dictionary<string, Symbol>();

        public Symbol Get(string identifier)
        {
            if (!_symbols.ContainsKey(identifier))
                throw new MissingSymbolException(identifier);

            return _symbols[identifier];
        }

        public Symbol Create(string identifier)
        {
            _symbols[identifier] = new Symbol(identifier);
            return _symbols[identifier];
        }
    }

    public class Symbol
    {
        public Symbol(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
        public int IntegerValue { get; set; }
    }
}
