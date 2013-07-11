using System;


namespace Lexer
{
    public class Token
    {
        private readonly string _name;
        private readonly TokenType _type;


        public Token( string name, TokenType type )
        {
            _name = name;
            _type = type;
        }


        public string Name
        {
            get { return _name; }
        }

        public TokenType Type
        {
            get { return _type; }
        }


        public void CheckType( TokenType type )
        {
            if ( !IsType( type ) )
                throw new Exception( "Ожидается " + type );
        }


        public void CheckNameIdentify( string name )
        {
            if ( !IsNameIdentify( name ) )
                throw new Exception( "Ожидается идентификатор '" + name + "'" );
        }


        public bool IsNameIdentify( string name )
        {
            return Type == TokenType.Identify && String.Equals( Name, name, StringComparison.CurrentCultureIgnoreCase );
        }


        public bool IsType( TokenType type )
        {
            return _type == type;
        }
    }
}