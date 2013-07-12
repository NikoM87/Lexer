using System.Collections.Generic;

using Lexer;
using Lexer.Tree;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LexerTest.Analyzer
{
    [TestClass]
    public class SyntaxTest
    {
        [TestMethod]
        public void TestVariableList()
        {
            IEnumerable<Token> tokens = new List<Token>
            {
                TokenFactory.CreateIdentify( "var" ),
                TokenFactory.CreateIdentify( "a" ),
                TokenFactory.CreateColun(),
                TokenFactory.CreateIdentify( "integer" ),
                TokenFactory.CreateSemicolun(),
                TokenFactory.CreateIdentify( "b" ),
                TokenFactory.CreateColun(),
                TokenFactory.CreateIdentify( "float" ),
                TokenFactory.CreateSemicolun()
            };
            IEnumerator<Token> list = tokens.GetEnumerator();
            list.MoveNext();

            var variables = new VariableList();
            variables.Parse( list );

            var var1 = (Variable) variables.Items[0];

            Assert.AreEqual( "a", var1.Name );
            Assert.AreEqual( "integer", var1.Type );

            var var2 = (Variable) variables.Items[1];

            Assert.AreEqual( "b", var2.Name );
            Assert.AreEqual( "float", var2.Type );
        }


        [TestMethod]
        public void TestStatementSequence()
        {
            IEnumerable<Token> tokens = new List<Token>
            {
                TokenFactory.CreateIdentify( "begin" ),
                TokenFactory.CreateIdentify( "WriteLn" ),
                TokenFactory.CreateIdentify( "end" )
            };
            IEnumerator<Token> list = tokens.GetEnumerator();
            list.MoveNext();

            var statementSequence = new StatementSequence();

            statementSequence.Parse( list );

            Assert.AreEqual( 1, statementSequence.Items.Count );
            Assert.AreEqual( typeof ( Statement ), statementSequence.Items[0].GetType() );
        }
    }
}