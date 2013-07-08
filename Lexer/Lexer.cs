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

        private StringBuilder lexem = new StringBuilder();
            

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

            if ( IsRelationalOperator() )
            {
                return ReadRelationalOperator();
            }

            if ( IsOperation() )
            {
                return ReadOperation();
            }

            if ( IsColon() )
            {
                return ReadColonOrAssign();
            }

            if ( IsStringConstant() )
            {
                return ReadStringConstant();
            }

            return "";
        }

        private string ReadStringConstant()
        {
            lexem.Clear();
            lexem.Append( _nextCh );
            NextChar();
            while ( _nextCh != '\'' )
            {
                lexem.Append( _nextCh );
                NextChar();
            }
            lexem.Append( _nextCh );
            NextChar();

            return lexem.ToString();
        }

        private bool IsStringConstant()
        {
            return _nextCh == '\'';
        }

        private string ReadColonOrAssign()
        {
            lexem.Clear();
            lexem.Append( _nextCh );
            NextChar();

            if ( _nextCh == '=' )
            {
                lexem.Append( _nextCh );
                NextChar();
            }

            return lexem.ToString();
        }

        private bool IsColon()
        {
            return _nextCh == ':';
        }

        private string ReadRelationalOperator()
        {
            lexem.Clear();
            lexem.Append( _nextCh );
            NextChar();
            if ( _nextCh == '>' || _nextCh == '=' )
            {
                lexem.Append( _nextCh );
                NextChar();
            }
            return lexem.ToString();
        }

        private bool IsRelationalOperator()
        {
            return  _nextCh == '='   || _nextCh == '>' || _nextCh == '<';
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
            lexem.Clear();
            lexem.Append( _nextCh );
            NextChar();
            return lexem.ToString();
        }

        private bool IsOperation()
        {
            return _nextCh == '+' || _nextCh == '-' || _nextCh == '*' || _nextCh == '/';
        }

        private string ReadIdentificator()
        {
            lexem.Clear();
            while ( _nextCh == '_' || char.IsLetterOrDigit( _nextCh ) )
            {
                lexem.Append( _nextCh );
                NextChar();
            }
            return lexem.ToString();
        }

        private bool IsIdentificator()
        {
            return _nextCh == '_' || char.IsLetter( _nextCh );
        }

        private string ReadIntNumber(  )
        {
            lexem.Clear();
            while ( char.IsNumber( _nextCh ) )
            {
                lexem.Append( _nextCh );
                NextChar();
            }
            return lexem.ToString();
        }

        private bool IsIntNumber()
        {
            return char.IsNumber( _nextCh );
        }
    }
}
