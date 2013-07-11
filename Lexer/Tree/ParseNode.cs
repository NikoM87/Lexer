using System.Collections.Generic;


namespace Lexer.Tree
{
    public class ParseNode
    {
        public List<ParseNode> Items;


        public ParseNode()
        {
            Items = new List<ParseNode>();
        }


        public void AddChild( ParseNode node )
        {
            Items.Add( node );
        }
    }
}