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
public class Section {

    public String name;
    private List<Data> data;

    public Section(String name, List<Data> data) {
        this.name = name;
        this.data = data;
    }

    public Section(String name) {
        this.name = name;
        this.data = new ArrayList<>();
    }

    public boolean add(Data data) {
        if (!exists(data.key)) {
            this.data.add(data);
            return true;
        }
        return false;
    }

    public void removeAll() {
        data.clear();
    }

    public boolean remove(String key) {
        for (var d : data) {
            if (d.key.equals(key)) {
                data.remove(d);
                return true;
            }
        }
        return false;
    }

    public List<Data> findAll() {
        return data;
    }

    public Data find(String key) {
        for (var d : data) {
            if (d.key.equals(key)) {
                return d;
            }
        }
        return null;
    }

    public boolean exists(String key) {
        return find(key) != null;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

}
