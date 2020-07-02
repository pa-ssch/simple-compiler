using Compiler.Exceptions;
using System.Collections.Generic;

namespace Compiler
{
    public class SymbolTable
    {
        private readonly Dictionary<string, Symbol> _symbols = new Dictionary<string, Symbol>();

        /// <summary>
        /// Returns a Symbol
        /// </summary>
        /// <param name="identifier">Identifier of the serarched Symbol</param>
        /// <exception cref="MissingSymbolException">Thrown when the searched Symbol is not declared</exception>
        public Symbol Get(string identifier)
        {
            if (!_symbols.ContainsKey(identifier))
                throw new MissingSymbolException(identifier);

            return _symbols[identifier];
        }

        /// <summary>
        /// Creates a new or overwrites an existing Symbol
        /// </summary>
        /// <param name="identifier">Name of the Symbol</param>
        /// <returns>The created or changed Symbol</returns>
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
