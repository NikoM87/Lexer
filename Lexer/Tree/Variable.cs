namespace Lexer.Tree
{
    public class Variable : ParseNode
    {
        public Variable( string name, string type )
        {
            Name = name;
            Type = type;
        }


        public Variable()
            : this( string.Empty, string.Empty )
        {
        }


        public string Name { get; set; }

        public string Type { get; set; }
    }
}