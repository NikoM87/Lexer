using System.Collections.Generic;


namespace Lexer.Tree
{
    public abstract class ParseNode
    {
        public List<ParseNode> Items;


        protected ParseNode()
        {
            Items = new List<ParseNode>();
        }


        public void AddChild( ParseNode node )
        {
            Items.Add( node );
        }


        public abstract void Parse( IEnumerator<Token> tokens );
    }
}