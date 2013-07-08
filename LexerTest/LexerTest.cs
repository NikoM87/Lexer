using System.IO;
using System.Text;

using Lexer;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerTest
{
    [TestClass]
    public class LexerTest
    {
        private readonly Lexer.Lexer _lex;


        public LexerTest()
        {
            _lex = new Lexer.Lexer();
        }


        [TestMethod]
        public void TestStreamLoadTextCode()
        {
            Stream stream = new MemoryStream( Encoding.ASCII.GetBytes( "Program" ) );
            var lex = new Lexer.Lexer( stream );

            Assert.AreEqual( stream, lex.BaseStream );
        }


        [TestMethod]
        public void TestSkipFirstWhitespace()
        {
            _lex.LoadTextCode( "\t Program" );

            Token word = _lex.NextToken();

            Assert.AreEqual( "Program", word.Name);
        }


        [TestMethod]
        public void TestTwoWord()
        {
            _lex.LoadTextCode( "Program HelloWorld" );

            Token word1 = _lex.NextToken();
            Token word2 = _lex.NextToken();

            Assert.AreEqual( "Program", word1.Name );
            Assert.AreEqual( "HelloWorld", word2.Name );
        }


        [TestMethod]
        public void TestIdentificator()
        {
            _lex.LoadTextCode( "__az_AZ09" );

            Token word = _lex.NextToken();

            Assert.AreEqual( "__az_AZ09", word.Name );
        }


        [TestMethod]
        public void TestOperation()
        {
            _lex.LoadTextCode( " + - * / " );

            Assert.AreEqual( "+", _lex.NextToken().Name );
            Assert.AreEqual( "-", _lex.NextToken().Name );
            Assert.AreEqual( "*", _lex.NextToken().Name );
            Assert.AreEqual( "/", _lex.NextToken().Name );
        }


        [TestMethod]
        public void TestRelationalOperator()
        {
            _lex.LoadTextCode( " = <> > >= < <= " );

            Assert.AreEqual( "=", _lex.NextToken().Name );
            Assert.AreEqual( "<>", _lex.NextToken().Name );
            Assert.AreEqual( ">", _lex.NextToken().Name );
            Assert.AreEqual( ">=", _lex.NextToken().Name );
            Assert.AreEqual( "<", _lex.NextToken().Name );
            Assert.AreEqual( "<=", _lex.NextToken().Name );
        }


        [TestMethod]
        public void TestColonAndAssign()
        {
            _lex.LoadTextCode( " : := : :=" );

            Assert.AreEqual( ":", _lex.NextToken().Name );
            Assert.AreEqual( ":=", _lex.NextToken().Name );
            Assert.AreEqual( ":", _lex.NextToken().Name );
            Assert.AreEqual( ":=", _lex.NextToken().Name );
        }


        [TestMethod]
        public void TestStringConstant()
        {
            _lex.LoadTextCode( " 'Hello, Wordld!' " );

            Assert.AreEqual( "'Hello, Wordld!'", _lex.NextToken().Name );
        }


        [TestMethod]
        public void TestEmpty()
        {
            _lex.LoadTextCode( "" );

            Token word = _lex.NextToken();

            Assert.AreEqual( string.Empty, word.Name );
        }


        [TestMethod]
        public void TestEmptyWithWhitespace()
        {
            _lex.LoadTextCode( "  " );

            Token word = _lex.NextToken();

            Assert.AreEqual( string.Empty, word.Name );
        }
    }
}