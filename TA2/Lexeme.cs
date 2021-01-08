namespace TA2
{
    class Lexeme
    {
        string token;
        string lexeme;
        int start;
        int lenght;
        int position;

        public Lexeme(string token, string lexeme, int start, int lenght, int position)
        {
            this.token = token;
            this.lexeme = lexeme;
            this.start = start;
            this.lenght = lenght;
            this.position = position;
        }

        public string getToken() { return token; }
        public string getLexeme() { return lexeme; }
        public int getStart() { return start; }
        public int getLenght() { return lenght; }
        public int getPosition() { return position; }

        public override string ToString()
        {
            string str = token + " " + lexeme + " " + start + " " + lenght + " " + position;
            return str;
        }
    }
}
