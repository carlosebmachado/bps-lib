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

namespace BPS.Util
{
    internal class Token
    {
        internal const string TK_SECTION = "<section>";
        internal const string TK_DATA = "<data>";
        internal const string TK_NAME = "<name>";
        internal const string TK_CLOSE = "</>";

        internal const char SY_HASH = '#';
        internal const char SY_OPEN_BRACKETS = '[';
        internal const char SY_CLOSE_BRACKETS = ']';
        internal const char SY_LESS = '<';
        internal const char SY_GREATER = '>';
        internal const char SY_COMMA = ',';
        internal const char SY_COLON = ':';
        internal const char SY_DOT = '.';
        internal const char SY_MINUS = '-';
        internal const char SY_SPACE = ' ';
        internal const char SY_BAR = '/';
        internal const char SY_BAR_INV = '\\';
        internal const char SY_QUOTE = '\'';
        internal const char SY_DQUOTE = '\"';
        internal const char SY_TAB = '\t';
        internal const char SY_RETURN = '\r';
        internal const char SY_NEWLINE = '\n';

        internal const string FILE_HEADER = "# BPS File";
        internal const string FILE_TAB = "    ";

        internal const string FILENAME_EXTENSION = ".bps";

        internal static string[] Tokens =
        {
            TK_SECTION,
            TK_DATA,
            TK_NAME,
            TK_CLOSE
        };

        internal static string[] OTokens =
        {
            TK_SECTION,
            TK_DATA,
            TK_NAME
        };

        internal static string[] CTokens =
        {
            TK_CLOSE
        };

        internal static bool IsToken(string str)
        {
            foreach (string token in Tokens)
                if (token.Equals(str)) return true;
            return false;
        }

        internal static bool IsOToken(string str)
        {
            foreach (string token in OTokens)
                if (token.Equals(str)) return true;
            return false;
        }

        internal static bool IsCToken(string str)
        {
            foreach (string token in CTokens)
                if (token.Equals(str)) return true;
            return false;
        }
    }
}
