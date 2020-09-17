using System.Collections;

namespace cslox
{
    class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = new ArrayList();
        private int start = 0;
        private int current = 0;
        private int line = 1;

        Scanner(string source){
            this.source = source;
        }

        List<Token> scanTokens(){
            while (!isAtEnd()){
                // beginning of the next lexeme
                start = current;
                scanToken();
            }
            tokens.Add(new Token(EOF,"", null, null));
            return tokens;
        }

        private void scanToken(){
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN);
                case ')': addToken(TokenType.RIGHT_PAREN);
                case '{': addToken(TokenType.LEFT_BRACE);
                case '}': addToken(TokenType.RIGHT_BRACE);
                case ',': addToken(TokenType.COMMA);
                case '.': addToken(TokenType.DOT);
                case '-': addToken(TokenType.MINUS);
                case '+': addToken(TokenType.PLUS);
                case '*': addToken(TokenType.STAR);
                case ';': addToken(TokenType.SEMICOLON);   

                default:
                    Lox.error(line, "Unexpected character.");   
            }
        }

        private char advance(){
            current++;
            return source.Substring(current-1, 1);
        }

        private void addToken(TokenType type){
            addToken(type, null);
        }

        private void addToken(TokenType type, object literal){
            string text = source.Substring(start, current);
            tokens.Add(new Token(type, text, literal, line));
        }

        private bool isAtEnd(){
            return current >= source.Length;
        }

    }
}