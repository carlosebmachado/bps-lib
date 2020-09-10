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
package me.carlosmachado.bps;

import java.util.List;
import java.util.ArrayList;

/**
 *
 * @author Carlos Eduardo de Borba Machado
 */
public class File {

    private List<Section> sections;

    public File() {
        sections = new ArrayList<>();
    }

    public File(List<Section> sections) {
        this.sections = sections;
    }

    public boolean add(Section section) {
        if (!exists(section.name)) {
            sections.add(section);
            return true;
        }
        return false;
    }

    public void removeAll() {
        sections.clear();
    }

    public boolean remove(String name) {
        for (Section s : sections) {
            if (s.name.equals(name)) {
                sections.remove(s);
                return true;
            }
        }
        return false;
    }

    public List<Section> findAll() {
        return sections;
    }

    public Section find(String name) {
        for (var s : sections) {
            if (s.name.equals(name)) {
                return s;
            }
        }
        return null;
    }

    public boolean exists(String name) {
        return find(name) != null;
    }

}
