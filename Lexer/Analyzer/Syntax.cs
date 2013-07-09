using System;

using Lexer.Tree;


namespace Lexer.Analyzer
{
    public class Syntax
    {
        private readonly ILexical _lexical;

        private readonly ParseNode _parseTree;
        private Token _currentToken;


        public Syntax( ILexical lexical )
        {
            _lexical = lexical;
            _parseTree = new ParseNode();
        }


        public ParseNode ParseTree
        {
            get { return _parseTree; }
        }


        public void Parse()
        {
            _currentToken = _lexical.NextToken();

            if ( IsVariableList() )
            {
                _currentToken = _lexical.NextToken();

                ParseVariableList( _parseTree );
                return;
            }

            throw new Exception( "Не распознаный синтаксис" );
        }


        private bool IsVariableList()
        {
            return _currentToken.Type == TokenType.Identify && _currentToken.Name.ToLower() == "var";
        }


        private void ParseVariableList( ParseNode node )
        {
            var childNode = new VariableList();

            while( _currentToken.Type == TokenType.Identify )
            {
                ParseVariable( childNode );

                node.AddChild( childNode );
            }
        }


        private void ParseVariable( ParseNode node )
        {
            var var = new Variable();

            if ( _currentToken.Type != TokenType.Identify )
                throw new Exception( "Ожидается идентификатор" );

            var.Name = _currentToken.Name;

            _currentToken = _lexical.NextToken();
            if ( _currentToken.Type != TokenType.Colun )
                throw new Exception( "Ожидается ':'" );

            _currentToken = _lexical.NextToken();

            if ( _currentToken.Type != TokenType.Identify )
                throw new Exception( "Ожидается идентификатор" );

            var.Type = _currentToken.Name;

            node.AddChild( var );

            _currentToken = _lexical.NextToken();
            if ( _currentToken.Type != TokenType.Semicolun )
                throw new Exception( "Ожидается ';'" );

            _currentToken = _lexical.NextToken();
        }
    }
}