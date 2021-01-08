using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TA2
{
    class LexemeManager
    {
        List<Lexeme> lexemesList = null;
        DataTable lexemesTable = null;

        List<string> keywords = null;
        List<string> operators = null;
        List<char> ignored = new List<char>()
        {
            ' ','\n',Environment.NewLine.ToCharArray()[0],',','\'','\"','(',')',';'
        };

        public LexemeManager()
        {
        }

        public DataTable getLexemesTable(string query)
        {
            lexemesList = analyzeQuery(query);
            createTable();
            lexemesTable = fillTable(lexemesList);
            return lexemesTable;
        }

        private List<Lexeme> analyzeQuery(string query)
        {
            readFiles();
            lexemesList = new List<Lexeme>();
            char[] queryArr = query.ToCharArray();
            string lexeme = "";
            int lenght = 0;
            int position = 0;
            int tempCount = 0;
            for (int i = 0; i < queryArr.Length; i++)
            {
                if (ignored.Contains(queryArr[i]) || i + 1 == queryArr.Length)
                {
                    if (lexeme != "")
                    {
                        if (i + 1 == queryArr.Length && !ignored.Contains(queryArr[i]))
                        {
                            lexeme += queryArr[i];
                            lenght++;
                            tempCount++;
                        }
                        lexemesList.Add(new Lexeme(findToken(lexeme), lexeme, tempCount - lenght + 1, lenght, position));
                        lexeme = "";
                        lenght = 0;
                        position++;
                    }
                }
                else
                {
                    lexeme += queryArr[i];
                    lenght++;
                    tempCount++;
                }
            }
            return lexemesList;
        }

        private void readFiles()
        {
            keywords = new List<string>();
            using (StreamReader sr = new StreamReader("Keywords.txt"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    keywords.Add(str);
                }
            }
            operators = new List<string>();
            using (StreamReader sr = new StreamReader("Operators.txt"))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    operators.Add(str);
                }
            }
        }

        private string findToken(string lexeme)
        {
            string token = "---";
            if (keywords.Contains(lexeme.ToUpper()))
            {
                token = "Keyword";
            }
            else
            {
                if (operators.Contains(lexeme.ToUpper()))
                {
                    token = "Operator";
                }
                else
                {
                    try
                    {
                        Convert.ToDouble(lexeme);
                        token = "Number";
                    }
                    catch (Exception)
                    {
                        token = "ID";
                    }
                }
            }
            return token;
        }

        private DataTable fillTable(List<Lexeme> lexemesList)
        {
            for (int i = 0; i < lexemesList.Count; i++)
            {
                DataRow row = lexemesTable.NewRow();
                row[0] = lexemesList[i].getToken();
                row[1] = lexemesList[i].getLexeme();
                row[2] = lexemesList[i].getStart();
                row[3] = lexemesList[i].getLenght();
                row[4] = lexemesList[i].getPosition();
                lexemesTable.Rows.Add(row);
            }
            return lexemesTable;
        }

        private void createTable()
        {
            lexemesTable = new DataTable("Lexemes");
            lexemesTable.Columns.AddRange(new DataColumn[5] {
                new DataColumn("Token", typeof(String)),
                new DataColumn("Lexeme", typeof(String)),
                new DataColumn("Start", typeof(int)),
                new DataColumn("Lenght", typeof(int)),
                new DataColumn("Position", typeof(int))
            });
        }
    }
}
