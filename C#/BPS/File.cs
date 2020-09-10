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

using System.Collections.Generic;

namespace BPS
{
    public class File
    {
        #region Vars

        private readonly List<Section> _sections;

        #endregion Vars


        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public File()
        {
            _sections = new List<Section>();
        }

        /// <summary>
        /// Constructor with a list of sections
        /// </summary>
        /// <param name="sections">File sections</param>
        public File(List<Section> sections)
        {
            _sections = sections;
        }

        /// <summary>
        /// Constructor with a list of sections
        /// </summary>
        /// <param name="sections">File sections</param>
        public File(Section[] sections)
        {
            _sections = new List<Section>(sections);
        }

        #endregion Constructors


        #region Methods

        /// <summary>
        /// Adds a new Data in the section
        /// </summary>
        /// <param name="section">Section to be add</param>
        /// <returns>If can add will return true, else false</returns>
        public bool AddSection(Section section)
        {
            if (!SectionExists(section.Name))
            {
                if (section.Name.Equals(""))
                    section.Name = _sections.Count.ToString();
                _sections.Add(section);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes all content of a BPS file
        /// </summary>
        public void ClearSections()
        {
            _sections.Clear();
        }

        /// <summary>
        /// Removes the section that contains the name passed by parameter
        /// </summary>
        /// <param name="name">Name to remove</param>
        /// <returns>If can remove will return true, else false</returns>
        public bool RemoveSection(string name)
        {
            foreach (Section s in _sections)
                if (s.Name.Equals(name))
                    return _sections.Remove(s);
            return false;
        }

        public bool RemoveSection(Section section)
        {
            return _sections.Remove(section);
        }

        /// <summary>
        /// Finds all sections in file
        /// </summary>
        /// <returns>A array with all sections</returns>
        public Section[] AllSections()
        {
            return _sections.ToArray();
        }

        /// <summary>
        /// Finds a unique data that contains the key passed by parameter
        /// </summary>
        /// <param name="name">Name to find</param>
        /// <returns>The section if finds it, else return null</returns>
        public Section FindSection(string name)
        {
            foreach (Section s in _sections)
                if (s.Name.Equals(name))
                    return s;
            return null;
        }

        public Section FindSection(Section section)
        {
            foreach (Section s in _sections)
                if (s == section)
                    return s;
            return null;
        }

        /// <summary>
        /// Check if a section exists
        /// </summary>
        /// <param name="name">A name to check</param>
        /// <returns>True if exists, else false</returns>
        public bool SectionExists(string name)
        {
            return FindSection(name) != null;
        }

        public bool SectionExists(Section section)
        {
            return FindSection(section) != null;
        }

        #endregion Methods

    }
}