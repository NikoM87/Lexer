using System.Collections.Generic;

using Lexer;
using Lexer.Analyzer;
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

            var parser = new Syntax( tokens );

            parser.Parse();
            var variables = (VariableList) parser.ParseTree.Items[0];

            Assert.AreEqual( 2, variables.Items.Count );

            var var1 = (Variable) variables.Items[0];

            Assert.AreEqual( "a", var1.Name );
            Assert.AreEqual( "integer", var1.Type );

            var var2 = (Variable) variables.Items[1];

            Assert.AreEqual( "b", var2.Name );
            Assert.AreEqual( "float", var2.Type );
        }
    }
}