using System.IO;
using System.Text;

namespace Lexer
{
    public class Lexer
    {
        private readonly StringBuilder _lexem = new StringBuilder();
        private readonly StreamReader _reader;
        private int _nextCh;


        public Lexer( Stream stream )
        {
            BaseStream = stream;
            _reader = new StreamReader( BaseStream );
            NextChar();
        }


        public Lexer()
        {
            BaseStream = new MemoryStream();
            _reader = new StreamReader( BaseStream );
        }


        public Stream BaseStream { get; private set; }


        public void LoadTextCode( string code )
        {
            var writer = new BinaryWriter( BaseStream );
            BaseStream.Position = 0;
            writer.Write( Encoding.ASCII.GetBytes( code ) );
            BaseStream.Position = 0;

            NextChar();
        }


        private void NextChar()
        {
            _nextCh = _reader.Read();
        }


        public Token NextToken()
        {
            SkipWhitespace();

            _lexem.Clear();

            if ( IsNumber() )
            {
                while ( IsNumber() )
                {
                    SupplementLexeme();
                }
            }
            else if ( IsEquals( '_' ) || IsLetter() )
            {
                while ( IsEquals( '_' ) || IsLetter() || IsNumber() )
                {
                    SupplementLexeme();
                }
            }
            else if ( IsEquals( '>' ) || IsEquals( '=' ) || IsEquals( '<' ) )
            {
                SupplementLexeme();
                if ( IsEquals( '>' ) || IsEquals( '=' ) )
                {
                    SupplementLexeme();
                }
            }
            else if ( IsMathOperation() )
            {
                SupplementLexeme();
            }
            else if ( IsEquals( ':' ) )
            {
                SupplementLexeme();
                if ( IsEquals( '=' ) )
                {
                    SupplementLexeme();
                }
            }
            else if ( IsEquals( '\'' ) )
            {
                SupplementLexeme();
                while ( !IsEquals( '\'' ) )
                {
                    SupplementLexeme();
                }
                SupplementLexeme();
            }

            return new Token( _lexem.ToString() );
        }


        private void SupplementLexeme()
        {
            _lexem.Append( (char) _nextCh );
            NextChar();
        }


        private bool IsEquals( char ch )
        {
            return _nextCh == ch;
        }


        private void SkipWhitespace()
        {
            while ( !_reader.EndOfStream && char.IsWhiteSpace( (char) _nextCh ) )
            {
                NextChar();
            }
        }


        private bool IsMathOperation()
        {
            return _nextCh == '+' || _nextCh == '-' || _nextCh == '*' || _nextCh == '/';
        }


        private bool IsLetter()
        {
            return ( ( 'A' <= _nextCh ) && ( _nextCh <= 'Z' ) ) || ( ( 'a' <= _nextCh ) && ( _nextCh <= 'z' ) );
        }


        private bool IsNumber()
        {
            return ( '0' <= _nextCh ) && ( _nextCh <= '9' );
        }
    }
}