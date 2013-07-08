using System.IO;
using System.Text;

namespace Lexer
{
    public class Lexer
    {
        private readonly StringBuilder _lexem = new StringBuilder();
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
            _nextCh = (char) _reader.Read();
        }


        public string NextLexeme()
        {
            SkipWhitespace();

            _lexem.Clear();

            if ( IsNumber() )
            {
                ReadNumbers();
            }
            else if ( IsIdentificator() )
            {
                ReadIdentificator();
            }
            else if ( IsRelationalOperator() )
            {
                ReadRelationalOperator();
            }
            else if ( IsMathOperation() )
            {
                ReadOperation();
            }
            else if ( IsColon() )
            {
                SupplementLexeme();
                if ( IsEquals( '=' ) )
                {
                    SupplementLexeme();
                }
            }
            else if ( IsSingleQuote() )
            {
                ReadStringConstant();
            }

            return _lexem.ToString();
        }


        private void SupplementLexeme()
        {
            _lexem.Append( _nextCh );
            NextChar();
        }


        private bool IsEquals( char ch )
        {
            return _nextCh == ch;
        }


        private void ReadStringConstant()
        {
            SupplementLexeme();
            while ( !IsSingleQuote() )
            {
                SupplementLexeme();
            }
            SupplementLexeme();
        }


        private bool IsSingleQuote()
        {
            return _nextCh == '\'';
        }


        private bool IsColon()
        {
            return _nextCh == ':';
        }


        private void ReadRelationalOperator()
        {
            SupplementLexeme();
            if ( _nextCh == '>' || _nextCh == '=' )
            {
                SupplementLexeme();
            }
        }


        private bool IsRelationalOperator()
        {
            return _nextCh == '=' || _nextCh == '>' || _nextCh == '<';
        }


        private void SkipWhitespace()
        {
            while ( !_reader.EndOfStream && char.IsWhiteSpace( _nextCh ) )
            {
                NextChar();
            }
        }


        private void ReadOperation()
        {
            SupplementLexeme();
        }


        private bool IsMathOperation()
        {
            return _nextCh == '+' || _nextCh == '-' || _nextCh == '*' || _nextCh == '/';
        }


        private void ReadIdentificator()
        {
            while ( _nextCh == '_' || char.IsLetterOrDigit( _nextCh ) )
            {
                SupplementLexeme();
            }
        }


        private bool IsIdentificator()
        {
            return _nextCh == '_' || char.IsLetter( _nextCh );
        }


        private void ReadNumbers()
        {
            while ( IsNumber() )
            {
                SupplementLexeme();
            }
        }


        private bool IsNumber()
        {
            return char.IsNumber( _nextCh );
        }
    }
}