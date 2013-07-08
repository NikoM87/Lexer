using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerTest
{
    [TestClass]
    public class LexerTest
    {
        private readonly Lexer.Lexer _lex;

        public LexerTest()
        {
            _lex = new Lexer.Lexer( );
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
            _lex.LoadTextCode( "\t Program");

            var word = _lex.Next();

            Assert.AreEqual( "Program",word  );
        }

        [TestMethod]
        public void TestTwoWord()
        {
            _lex.LoadTextCode( "Program HelloWorld" );

            var word1 = _lex.Next();
            var word2 = _lex.Next();

            Assert.AreEqual( "Program", word1 );
            Assert.AreEqual( "HelloWorld", word2 );
        }

        [TestMethod]
        public void TestIdentificator()
        {
            _lex.LoadTextCode( "__az_AZ09" );

            var word = _lex.Next();

            Assert.AreEqual( "__az_AZ09", word );
        }

        [TestMethod]
        public void TestOperation()
        {
            _lex.LoadTextCode( " + - * / " );

            Assert.AreEqual( "+", _lex.Next() );
            Assert.AreEqual( "-", _lex.Next() );
            Assert.AreEqual( "*", _lex.Next() );
            Assert.AreEqual( "/", _lex.Next() );
        }


        [TestMethod]
        public void TestRelationalOperator()
        {
            _lex.LoadTextCode( " = <> > >= < <= " );

            Assert.AreEqual( "=", _lex.Next() );
            Assert.AreEqual( "<>", _lex.Next() );
            Assert.AreEqual( ">", _lex.Next() );
            Assert.AreEqual( ">=", _lex.Next() );
            Assert.AreEqual( "<", _lex.Next() );
            Assert.AreEqual( "<=", _lex.Next() );
        }


        [TestMethod]
        public void TestColonAndAssign()
        {
            _lex.LoadTextCode( " : := : :=" );

            Assert.AreEqual( ":", _lex.Next() );
            Assert.AreEqual( ":=", _lex.Next() );
            Assert.AreEqual( ":", _lex.Next() );
            Assert.AreEqual( ":=", _lex.Next() );
        }


        [TestMethod]
        public void TestStringConstant()
        {
            _lex.LoadTextCode( " 'Hello, Wordld!' " );

            Assert.AreEqual( "'Hello, Wordld!'", _lex.Next() );
        }


        [TestMethod]
        public void TestEmpty()
        {
            _lex.LoadTextCode( "" );

            var word = _lex.Next();

            Assert.AreEqual( string.Empty, word );
        }

        [TestMethod]
        public void TestEmptyWithWhitespace()
        {
            _lex.LoadTextCode( "  " );

            var word = _lex.Next();

            Assert.AreEqual( string.Empty, word );
        }
    }
}
