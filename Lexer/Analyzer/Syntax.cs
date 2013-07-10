using System;
using System.Collections.Generic;

using Lexer.Tree;


namespace Lexer.Analyzer
{
    public class Syntax
    {
        private readonly IEnumerator<Token> _lexical;

        private readonly ParseNode _parseTree;


        public Syntax( IEnumerable<Token> lexical )
        {
            _lexical = lexical.GetEnumerator();
            _parseTree = new ParseNode();
        }


        public ParseNode ParseTree
        {
            get { return _parseTree; }
        }


        public void Parse()
        {
            _lexical.MoveNext();

            if ( IsVariableList() )
            {
                _lexical.MoveNext();

                ParseVariableList( _parseTree );
                return;
            }

            throw new Exception( "Не распознаный синтаксис" );
        }


        private bool IsVariableList()
        {
            return _lexical.Current.Type == TokenType.Identify && _lexical.Current.Name.ToLower() == "var";
        }


        private void ParseVariableList( ParseNode node )
        {
            var childNode = new VariableList();

            while( _lexical.Current != null && _lexical.Current.Type == TokenType.Identify )
            {
                ParseVariable( childNode );

                node.AddChild( childNode );
            }
        }


        private void ParseVariable( ParseNode node )
        {
            var var = new Variable();

            _lexical.Current.CheckType( TokenType.Identify );
            var.Name = _lexical.Current.Name;

            _lexical.MoveNext();
            _lexical.Current.CheckType( TokenType.Colun );

            _lexical.MoveNext();
            _lexical.Current.CheckType( TokenType.Identify );
            var.Type = _lexical.Current.Name;

            node.AddChild( var );

            _lexical.MoveNext();
            _lexical.Current.CheckType( TokenType.Semicolun );

            _lexical.MoveNext();
        }
    }
}