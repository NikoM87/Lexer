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
            _parseTree = new Block();
        }


        public ParseNode ParseTree
        {
            get { return _parseTree; }
        }


        public void Parse()
        {
            _parseTree.Parse( _lexical );
        }
    }
}