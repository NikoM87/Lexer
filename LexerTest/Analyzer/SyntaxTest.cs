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
            var lexical = new Lexical();
            lexical.LoadTextCode( "var " +
                                  "a: integer;" +
                                  "b: float;" );

            var parser = new Syntax( lexical );

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