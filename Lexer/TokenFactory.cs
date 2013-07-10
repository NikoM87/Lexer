namespace Lexer
{
    public class TokenFactory
    {
        public static Token CreateIdentify( string name )
        {
            return new Token( name, TokenType.Identify );
        }


        public static Token CreateColun()
        {
            return new Token( ":", TokenType.Colun );
        }


        public static Token CreateSemicolun()
        {
            return new Token( ";", TokenType.Semicolun );
        }
    }
}