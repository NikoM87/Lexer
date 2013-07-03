using System.IO;
using System.Text;

namespace Lexer
{
    public class Lexer
    {
        public Stream BaseStream { get; private set; }

        public void LoadTextCode( string code )
        {

            BinaryWriter writer = new BinaryWriter( BaseStream );
            BaseStream.Position = 0;
            writer.Write( Encoding.ASCII.GetBytes( code ) );
            BaseStream.Position = 0;

            NextChar();
        }

        private void NextChar()
        {
            _nextCh = (char) _reader.Read();

        }

        private readonly StreamReader _reader;
        private char _nextCh;

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

        public string Next()
        {
            SkipWhitespace();

            if ( IsIntNumber() )
            {
                return ReadIntNumber();
            }

            if ( IsIdentificator() )
            {
                return ReadIdentificator();
            }

            if ( IsOperation() )
            {
                return ReadOperation();
            }

            return "";
        }

        private void SkipWhitespace()
        {
            while ( !_reader.EndOfStream && char.IsWhiteSpace( _nextCh ) )
            {
                NextChar();
            }
        }

        private string ReadOperation()
        {
            var s = "";
            s += _nextCh;
            NextChar();
            return s;
        }

        private bool IsOperation()
        {
            return _nextCh == '+' || _nextCh == '-' || _nextCh == '*' || _nextCh == '/';
        }

        private string ReadIdentificator()
        {
            var s = "";
            while ( _nextCh == '_' || char.IsLetterOrDigit( _nextCh ) )
            {
                s += _nextCh;
                NextChar();
            }
            return s;
        }

        private bool IsIdentificator()
        {
            return _nextCh == '_' || char.IsLetter( _nextCh );
        }

        private string ReadIntNumber(  )
        {
            var s = "";
            while ( char.IsNumber( _nextCh ) )
            {
                s += _nextCh;
                NextChar();
            }
            return s;
        }

        private bool IsIntNumber()
        {
            return char.IsNumber( _nextCh );
        }
    }
}
