/*
 * MIT License
 *
 * Copyright (c) 2020 Carlos Eduardo de Borba Machado
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */


using System;
using System.Collections.Generic;
using System.Globalization;

namespace BPS.Util
{
    internal class Compiler
    {
        static Compiler()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        internal static File Compile(string data)
        {
            var tokens = Lexer(data);
            var sections = Parser(tokens);
            return new File(sections);
        }

        private static List<string> Lexer(string data)
        {
            var tokens = new List<string>();

            string cleanString = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Equals(Token.SY_DQUOTE))
                {
                    cleanString += data[i];
                    i++;
                    while (!data[i].Equals(Token.SY_DQUOTE) &&
                        !data[i - 1].Equals(Token.SY_BAR_INV))
                    {
                        cleanString += data[i];
                        i++;
                    }
                }
                if (data[i].Equals(Token.SY_HASH))
                    while (!data[i].Equals(Token.SY_NEWLINE)) i++;
                if (data[i].Equals(Token.SY_RETURN) ||
                    data[i].Equals(Token.SY_NEWLINE) ||
                    data[i].Equals(Token.SY_SPACE) ||
                    data[i].Equals(Token.SY_TAB)) continue;
                cleanString += data[i];
            }

            string curWord = "";
            bool hasSomething = false;
            for (int i = 0; i < cleanString.Length; i++)
            {
                if (cleanString[i].Equals(Token.SY_LESS) && !hasSomething)
                {
                    hasSomething = true;
                }
                if (hasSomething)
                {
                    if (cleanString[i].Equals(Token.SY_COLON))
                    {
                        tokens.Add(curWord);
                        tokens.Add(cleanString[i].ToString());
                        curWord = "";
                        continue;
                    }
                    if (cleanString[i].Equals(Token.SY_LESS) &&
                        cleanString[i + 1].Equals(Token.SY_BAR))
                    {
                        if(!curWord.Equals("")) tokens.Add(curWord);
                        curWord = cleanString[i].ToString();
                        continue;
                    }
                    curWord += cleanString[i];
                    if (Token.IsToken(curWord))
                    {
                        tokens.Add(curWord);
                        if (!Token.IsOToken(curWord))
                            hasSomething = false;
                        curWord = "";
                    }
                }
            }
            return tokens;
        }

        private static List<Section> Parser(List<string> tokens)
        {
            var sections = new List<Section>();

            for (int i = 0; i < tokens.Count; i++)
            {
                // verifica a abertura de uma nova section
                if (tokens[i].Equals(Token.TK_SECTION))
                {
                    var section = new Section(Token.SY_DQUOTE + sections.Count.ToString() + Token.SY_DQUOTE);
                    i++;
                    // verifica se a tag irá possuir um nome
                    if (tokens[i].Equals(Token.TK_NAME))
                    {
                        i++;
                        // verifica se há algum token ao invés de um nome
                        // um nome não pode ficar vazio
                        if (Token.IsToken(tokens[i]))
                            throw new Exception("Expected a name, not " + tokens[i]);
                        // verifica se é string
                        else if (!tokens[i][0].Equals(Token.SY_DQUOTE))
                            throw new Exception("Expected a string in name " + tokens[i]);
                        else
                            section.Name = tokens[i];
                        i++;
                        // verifica se a tag name é fechada
                        if (Token.IsCToken(tokens[i]))
                            i++;
                        else
                            throw new Exception("Expected a </> tag, not " + tokens[i]);
                    }
                    // busca os dados de uma section
                    while (tokens[i].Equals(Token.TK_DATA))
                    {
                        string key;
                        string value;
                        i++;
                        // verifica se exise uma key
                        if (Token.IsToken(tokens[i]))
                            throw new Exception("Expected a key, not " + tokens[i]);
                        key = tokens[i];
                        i++;
                        // varifica se existe o simbolo de atribuição
                        if (!tokens[i].Equals(Token.SY_COLON.ToString()))
                            throw new Exception("Expected a atribuition symbol ':' and was found '" + tokens[i] + "'");
                        i++;
                        // le o valor
                        if (Token.IsToken(tokens[i]))
                            throw new Exception("Expected a value, not " + tokens[i]);
                        value = tokens[i];
                        i += 2;
                        section.AddData(ParseData(key, value));
                    }
                    sections.Add(section);
                }
                else
                {
                    // caso não seja uma tag de section, gera um erro
                    throw new Exception("Expected a <section> tag, not " + tokens[i]);
                }
            }

            return sections;
        }

        private static Data ParseData(string key, string value)
        {
            if (IsString(value))
                return new Data(key, ParseString(value));
            if (IsChar(value))
                return new Data(key, ParseChar(value));
            if (IsBool(value))
                return new Data(key, ParseBool(value));
            if (IsInt(value))
                return new Data(key, int.Parse(value));
            if (IsDouble(value))
                return new Data(key, double.Parse(value));
            if (IsArray(value))
            {
                var data = new List<object>();
                value = value.Substring(1, value.Length - 2);
                var rawArr = value.Split(Token.SY_COMMA);
                foreach (var ri in rawArr)
                {
                    if (IsString(ri))
                        data.Add(ParseString(ri));
                    if (IsChar(ri))
                        data.Add(ParseChar(ri));
                    if (IsBool(ri))
                        data.Add(ParseBool(ri));
                    if (IsInt(ri))
                        data.Add(int.Parse(ri));
                    if (IsDouble(ri))
                        data.Add(double.Parse(ri));
                }
                return new Data(key, data);
            }
            return null;
        }

        private static string ParseString(string value)
        {
            return value.Substring(1, value.Length - 2);
        }

        private static char ParseChar(string value)
        {
            return char.Parse(value.Substring(1, 1));
        }

        private static bool ParseBool(string value)
        {
            return value.Equals("true");
        }

        private static bool IsString(string value)
        {
            return value[0].Equals(Token.SY_DQUOTE);
        }

        private static bool IsChar(string value)
        {
            return value[0].Equals(Token.SY_QUOTE) && value.Length == 3;
        }

        private static bool IsBool(string value)
        {
            return value.Equals("true") || value.Equals("false");
        }

        private static bool IsNumeric(string value)
        {
            return char.IsDigit(value[0]) || value[0].Equals(Token.SY_MINUS);
        }

        private static bool IsInt(string value)
        {
            if (IsNumeric(value)) {
                foreach (var c in value)
                    if (c.Equals(Token.SY_DOT))
                        return false;
                return true;
            }
            return false;
        }

        private static bool IsDouble(string value)
        {
            if(IsNumeric(value))
                foreach (var c in value)
                    if (c.Equals(Token.SY_DOT))
                        return true;
            return false;
        }

        private static bool IsArray(string value)
        {
            return value[0].Equals(Token.SY_OPEN_BRACKETS);
        }
    }
}
