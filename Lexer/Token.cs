namespace Lexer
{
    public class Token
    {
        private readonly string _name;


        public Token( string name )
        {
            _name = name;
        }


        public string Name
        {
            get { return _name; }
        }
    }
}