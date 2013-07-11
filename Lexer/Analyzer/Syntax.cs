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

            if ( _lexical.Current.IsNameIdentify( "var" ) )
            {
                ParseVariableList( _parseTree );
                return;
            }

            if ( _lexical.Current.IsNameIdentify( "begin" ) )
            {
                ParseBlock( _parseTree );
                return;
            }

            throw new Exception( "Не распознаный синтаксис" );
        }


        private void ParseBlock( ParseNode node )
        {
            _lexical.Current.CheckNameIdentify( "begin" );
            _lexical.MoveNext();

            var statementSequence = new StatementSequence();
            while( _lexical.Current != null && !_lexical.Current.IsNameIdentify( "end" ) )
            {
                statementSequence.AddChild( new Statement() );
                _lexical.MoveNext();
            }
            node.AddChild( statementSequence );
        }


        private void ParseVariableList( ParseNode node )
        {
            _lexical.Current.CheckNameIdentify( "var" );

            _lexical.MoveNext();

            var variableList = new VariableList();

            while( _lexical.Current != null && _lexical.Current.IsType( TokenType.Identify ) )
            {
                ParseVariable( variableList );
                _lexical.MoveNext();

                node.AddChild( variableList );
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
        }
    }
}