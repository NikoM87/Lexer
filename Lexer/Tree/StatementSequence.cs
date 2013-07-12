using System.Collections.Generic;


namespace Lexer.Tree
{
    public class StatementSequence : ParseNode
    {
        public override void Parse( IEnumerator<Token> tokens )
        {
            tokens.Current.CheckNameIdentify( "begin" );
            tokens.MoveNext();

            while( tokens.Current != null && !tokens.Current.IsNameIdentify( "end" ) )
            {
                AddChild( new Statement() );
                tokens.MoveNext();
            }
        }
    }
}