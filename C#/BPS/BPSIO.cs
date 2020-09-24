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
using System.IO;
using BPS.Util;

namespace BPS
{
    public class BPSIO
    {
        #region Vars

        #endregion Vars


        #region Methods

        #region Public

        /// <summary>
        /// Returns the readed BPS file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File readed</returns>
        public static File Read(string path)
        {
            string data;
            try
            {
                StreamReader file = new StreamReader(NormalizePath(path));
                data = file.ReadToEnd();
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Compiler.Compile(data);
        }

        /// <summary>
        /// Write a BPS file
        /// </summary>
        /// <param name="file">The file to be write</param>
        public static void Write(File file, string path)
        {
            try
            {
                StreamWriter wf = new StreamWriter(NormalizePath(path));

                wf.WriteLine(Token.FILE_HEADER + Token.SY_NEWLINE);
                foreach (Section section in file.AllSections())
                {
                    wf.WriteLine(Token.TK_SECTION);
                    wf.WriteLine(Token.FILE_TAB + Token.TK_NAME + Token.SY_DQUOTE + section.Name + Token.SY_DQUOTE + Token.TK_CLOSE);
                    foreach (Data data in section.AllData())
                    {
                        wf.Write(Token.FILE_TAB + Token.TK_DATA + data.Key + Token.SY_COLON);
                        if (data.Value is string strValue)
                        {
                            wf.Write(Token.SY_DQUOTE + strValue + Token.SY_DQUOTE);
                        }
                        else if (data.Value is char charValue)
                        {
                            wf.Write(Token.SY_QUOTE + charValue.ToString() + Token.SY_QUOTE);
                        }
                        else if (data.Value is bool boolValue)
                        {
                            if (boolValue) wf.Write("true");
                            else wf.Write("false");
                        }
                        else if (data.Value is List<object> listValue)
                        {
                            wf.Write(Token.SY_OPEN_BRACKETS);
                            for (int i = 0; i < listValue.Count; i++)
                            {
                                if (listValue[i] is string strv)
                                {
                                    wf.Write(Token.SY_DQUOTE + strv + Token.SY_DQUOTE);
                                }
                                else if (listValue[i] is char charv)
                                {
                                    wf.Write(Token.SY_QUOTE + charv.ToString() + Token.SY_QUOTE);
                                }
                                else if (listValue[i] is bool boolv)
                                {
                                    if (boolv) wf.Write("true");
                                    else wf.Write("false");
                                }
                                else wf.Write(listValue[i].ToString());
                                if (i < listValue.Count - 1) wf.Write(Token.SY_COMMA);
                            }
                            wf.Write(Token.SY_CLOSE_BRACKETS);
                        }
                        else wf.Write(data.Value);
                        wf.WriteLine(Token.TK_CLOSE);
                    }
                    wf.WriteLine(Token.TK_CLOSE + Token.SY_NEWLINE);
                }
                wf.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public


        #region Private

        /// <summary>
        /// Insert BPS extension on filename
        /// </summary>
        /// <param name="path">File path</param>
        private static string NormalizePath(string path)
        {
            int length = path.Length;
            if (length > 4)
            {
                if (!path.Substring(length - 4, 4).Equals(Token.FILENAME_EXTENSION))
                    return path + Token.FILENAME_EXTENSION;
                else
                    return path;
            }
            else if (path.Equals(Token.FILENAME_EXTENSION))
                return path;
            else
                return path + Token.FILENAME_EXTENSION;
        }

        #endregion Private

        #endregion Methods

    }
}