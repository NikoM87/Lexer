using System.Collections.Generic;


namespace Lexer.Tree
{
    public class VariableList : ParseNode
    {
        public override void Parse( IEnumerator<Token> tokens )
        {
            tokens.Current.CheckNameIdentify( "var" );
            tokens.MoveNext();

            while( tokens.Current != null && tokens.Current.IsType( TokenType.Identify ) )
            {
                var var = new Variable();

                var.Parse( tokens );

                AddChild( var );
                tokens.MoveNext();
            }
        }
    }
}