using System.Collections.Generic;


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


        public string Name { get; private set; }

        public string Type { get; private set; }


        public override void Parse( IEnumerator<Token> tokens )
        {
            tokens.Current.CheckType( TokenType.Identify );

            Name = tokens.Current.Name;

            tokens.MoveNext();
            tokens.Current.CheckType( TokenType.Colun );

            tokens.MoveNext();
            tokens.Current.CheckType( TokenType.Identify );

            Type = tokens.Current.Name;

            tokens.MoveNext();
            tokens.Current.CheckType( TokenType.Semicolun );
        }
    }
}