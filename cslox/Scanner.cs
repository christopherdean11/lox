using System.Collections;
using System.Collections.Generic;

namespace cslox
{
    public class Scanner
    {
        private readonly string source;
        private readonly List<Token> tokens = new List<Token>();
        private int start = 0;
        private int current = 0;
        private int line = 1;
        private static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>(){
            {"and", TokenType.AND},
            {"class", TokenType.CLASS},
            {"else", TokenType.ELSE},
            {"false", TokenType.FALSE},
            {"for", TokenType.FOR},
            {"fun", TokenType.FUN},
            {"if", TokenType.IF},
            {"nil", TokenType.NIL},
            {"or", TokenType.OR},
            {"print", TokenType.PRINT},
            {"return", TokenType.RETURN},
            {"super", TokenType.SUPER},
            {"this", TokenType.THIS},
            {"true", TokenType.TRUE},
            {"var", TokenType.VAR},
            {"while", TokenType.WHILE}
        };

        public Scanner(string source){
            this.source = source;
        }

        public List<Token> scanTokens(){
            while (!isAtEnd()){
                // beginning of the next lexeme
                start = current;
                scanToken();
            }
            tokens.Add(new Token(TokenType.EOF,"", null, -1));
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
                case '!': addToken(matchNext('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': addToken(matchNext('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
                case '<': addToken(matchNext('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '>': addToken(matchNext('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
                case '/':
                    if (matchNext('/')){
                        // its a comment for the rest of the line
                        while (peek() != '\n' && !isAtEnd()){
                            // get next character as long as not at the end of the line or end of file
                            advance();
                        }
                    } else {
                        addToken(TokenType.SLASH);
                    }
                    break;
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
                    } else if (isAlpha(c)){
                        identifier();
                    }
                     else {
                        Lox.error(line, "Unexpected character.");   
                    }
                    break;
            }
        }

        private void stringToken(){
            while (peek() != '"' && !isAtEnd()){
                if (peek() == '\n') line++;
                advance();
            }
            if (isAtEnd()){
                Lox.error(line, "Unterminated string found at end of file");
            }
            // advance one more to capture the closing " that was found by peek()
            advance();

            // strip the " from beginning and end of the token before adding token
            int len = (current-1) - (start+1) + 1;
            string s = new string(source.Substring(start + 1, len));
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
            int len = current - start;
            double d = System.Convert.ToDouble(source.Substring(start, len));
            addToken(TokenType.NUMBER, d);
        }

        private bool isDigit(char c){
            return c >= '0' && c <= '9';
        }

        private void identifier(){
            while (isAlphaNumeric(peek())) advance();
            int len = (current-1) - (start+1) + 1;
            string text = source.Substring(start, len);
            TokenType type;
            // try to get the value in "text" from the keywords dictionary
            // if if succeeds, type is updated as an out param
            // if it fails, it will return false, and then set type 
            // to an identifier
            if (!keywords.TryGetValue(text, out type)){
                type = TokenType.IDENTIFER;
            }

            addToken(type);
        }

        private bool isAlpha(char c){
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                   c == '_';
        }

        private bool isAlphaNumeric(char c){
            return isAlpha(c) || isDigit(c);
        }


        private char peek(){
            if (isAtEnd()) return '\0';
            return source[current];
        }

        private char peekNext(){
            if (current + 1 >= source.Length ) return '\0';
            return source[current+1];
        }

        private char advance(){
            current++;
            return source[current-1];
        }

        private bool matchNext(char expected){
            if (isAtEnd()) return false;
            if (source[current] != expected) return false;

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
            int len = current - start;
            string text = source.Substring(start, len);
            tokens.Add(new Token(type, text, literal, line));
        }

        private bool isAtEnd(){
            return current >= source.Length;
        }

    }
}