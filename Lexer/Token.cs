using Lexer.Analyzer;


namespace Lexer
{
    public class Token
    {
        private readonly string _name;
        private readonly TokenType _type;


        public Token( string name, TokenType type )
        {
            _name = name;
            _type = type;
        }


        public string Name
        {
            get { return _name; }
        }

        public TokenType Type
        {
            get { return _type; }
        }
    }
}