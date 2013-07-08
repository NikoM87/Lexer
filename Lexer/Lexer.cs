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

        private const int Max_Length_Of_Lexem = 16;

        private StringBuilder lexem = new StringBuilder( Max_Length_Of_Lexem );
            

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

            lexem.Clear();

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

            return string.Empty;
        }

        private void SupplementLexeme()
        {
            lexem.Append( _nextCh );
            NextChar();
        }

        private string ReadStringConstant()
        {
            SupplementLexeme();
            while ( _nextCh != '\'' )
            {
               SupplementLexeme();
            }
            SupplementLexeme();

            return lexem.ToString();
        }

        private bool IsStringConstant()
        {
            return _nextCh == '\'';
        }

        private string ReadColonOrAssign()
        {
            SupplementLexeme();

            if ( _nextCh == '=' )
            {
                SupplementLexeme();
            }

            return lexem.ToString();
        }

        private bool IsColon()
        {
            return _nextCh == ':';
        }

        private string ReadRelationalOperator()
        {
            SupplementLexeme();
            if ( _nextCh == '>' || _nextCh == '=' )
            {
                SupplementLexeme();
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
            SupplementLexeme();
            return lexem.ToString();
        }

        private bool IsOperation()
        {
            return _nextCh == '+' || _nextCh == '-' || _nextCh == '*' || _nextCh == '/';
        }

        private string ReadIdentificator()
        {
            while ( _nextCh == '_' || char.IsLetterOrDigit( _nextCh ) )
            {
                SupplementLexeme();
            }
            return lexem.ToString();
        }

        private bool IsIdentificator()
        {
            return _nextCh == '_' || char.IsLetter( _nextCh );
        }

        private string ReadIntNumber(  )
        {
            while ( char.IsNumber( _nextCh ) )
            {
                SupplementLexeme();
            }
            return lexem.ToString();
        }

        private bool IsIntNumber()
        {
            return char.IsNumber( _nextCh );
        }
    }
}
