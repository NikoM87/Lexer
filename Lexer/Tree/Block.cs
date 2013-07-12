using System.Collections.Generic;


namespace Lexer.Tree
{
    public class Block : ParseNode
    {
        public override void Parse( IEnumerator<Token> tokens )
        {
            tokens.MoveNext();

            ParseNode parseNode;

            if ( tokens.Current != null && tokens.Current.IsNameIdentify( "var" ) )
            {
                parseNode = new VariableList();
                parseNode.Parse( tokens );
                AddChild( parseNode );
            }

            if ( tokens.Current != null && tokens.Current.IsNameIdentify( "begin" ) )
            {
                parseNode = new StatementSequence();
                parseNode.Parse( tokens );
                AddChild( parseNode );
            }
        }
    }
}