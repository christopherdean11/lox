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
            tokens.Add(new Token(TokenType.EOF,"", null, null));
            return tokens;
        }

        private void scanToken(){
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case '*': addToken(TokenType.STAR); break;
                case ';': addToken(TokenType.SEMICOLON); break; 
                case '!': addToken(matchNext("=") ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': addToken(matchNext("=") ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
                case '<': addToken(matchNext("=") ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '>': addToken(matchNext("=") ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
                case '/':
                    if (matchNext('/')){
                        // its a comment for the rest of the line
                        while (peek() != '\n' && ~isAtEnd()){
                            // get next character as long as not at the end of the line or end of file
                            advance();
                        }
                    } else {
                        addToken(TokenType.SLASH);
                    }
                case ' ':
                case '\t':
                case '\r':
                    // ignore whitespace
                    break;
                case '\n':
                    line++;
                    break;
                case '"': stringToken(); break;
                default:
                    if (isDigit(c)){
                        numberToken();
                    } else {
                        Lox.error(line, "Unexpected character.");   
                    }
            }
        }

        private void stringToken(){
            while (peek() != '"' && !isAtEnd()){
                if (peek() == '\n') line++;
                advance();
            }
            if (isAtEnd()){
                Lox.error("Unterminated string found at end of file");
            }
            // advance one more to capture the closing " that was found by peek()
            advance();

            // strip the " from beginning and end of the token before adding token
            string s = new string(source.Substring(start + 1, current - 1));
            addToken(TokenType.STRING, s);
        }

        private void numberToken(){
            while (isDigit(peek())){
                advance();
            }
            // look for fractional part
            if (peek() == '.' && isDigit(peekNext())){
                // consume the dot
                advance();
                while (isDigit(peek())) advance();
            }
            double d = new System.Double.Parse(source.Substring(start, current));
            addToken(TokenType.NUMBER, d);
        }

        private bool isDigit(char c){
            return c >= '0' && c <= '9';
        }

        private char peek(){
            if (isAtEnd()) return '\0';
            return source.Substring(current,1);
        }

        private char peekNext(){
            if (current + 1 >= source.Length ) return '\0';
            return source.Substring(current+1,1);
        }

        private char advance(){
            current++;
            return source.Substring(current-1, 1);
        }

        private bool matchNext(char expected){
            if (isAtEnd()) return false;
            if (source.Substring(current,1) != expected) return false;

            // otherwise it is a match so increment current
            current++;
            // could call advance() here instead to be more clear
            // advance();
            return true;
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