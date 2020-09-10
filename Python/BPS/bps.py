#
# MIT License
#
# Copyright (c) 2020 Carlos Eduardo de Borba Machado
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.
#


ERR_OPEN_NEW_WOUT_CLOSE_PREV = "Trying to open a new section without closing the previous one."
ERR_CLOSE_WOUT_OPEN_PREV = "Section was closed without open it previously."

KV_HEADER = "# BPS File"
KV_NEXTLINE = "\n"
KV_LAB = "<"
KV_RAB = ">"
KV_TAB = "    "
KV_SEPARATOR = ":"
FILENAME_EXTENSION = ".bps"


class Data:
    key: str
    value: str

    def __init__(self, key: str, value: str):
        self.key = key
        self.value = value


class Section:
    name: str
    data: [str]

    def __init__(self, name: str, data: [str]):
        self.name = name
        self.data = data

    def __init__(self, name: str):
        self.name = name
        self.data = []

    def add(self, data: Data) -> bool:
        if not self.exists(data.key):
            self.data.append(data)
            return True
        return False

    def remove_all(self):
        self.data = []

    def remove(self, key: str) -> bool:
        for d in self.data:
            if d.key == key:
                self.data.remove(d)
                return True
        return False

    def find_all(self):
        return self.data

    def find(self, key: str) -> Data:
        for d in self.data:
            if d.key == key:
                return d
        return None

    def exists(self, key: str) -> bool:
        return self.find(key) != None


class File:
    sections: [Section]

    def __init__(self):
        self.sections = []

    def __init__(self, sections: [Section]):
        self.sections = sections

    def add(self, section: Section) -> bool:
        if not self.exists(section.name):
            self.sections.append(section)
            return True
        return False

    def remove_all(self):
        self.sections = []

    def remove(self, name: str) -> bool:
        for s in self.sections:
            if s.name == name:
                self.sections.remove(s)
                return True
        return False

    def find_all(self):
        return self.sections

    def find(self, name: str) -> Section:
        for s in self.sections:
            if s.name == name:
                return s
        return None

    def exists(self, name: str) -> bool:
        return self.find(name) != None


def read(path: str) -> File:
    data = []
    raw_sections = []
    sections: [Section] = []
    op = False

    r = open(normalize(path), 'r')
    for line in r:
        if line == "":
            continue
        if line[0] == ' ':
            line = line.strip()
        line = remove_comments(line)
        if line == "":
            continue
        if line[0] == ' ':
            line = line.strip()
        if line[0] == '#':
            continue
        line = line.strip()
        if line == "":
            continue
        data.append(line)
    r.close()

    for d in data:
        if d[0] == '<':
            if op:
                return None
            else:
                op = True
        if d == ">":
            if not op:
                return None
            else:
                raw_sections.append([])
                op = False

    for rs in raw_sections:
        while True:
            cur_line = data[0]
            del data[0]
            rs.append(cur_line)
            if cur_line == ">": break

    for rs in raw_sections:
        while not rs[0][0] == '<':
            del rs[0]
        for s in rs:
            if s[0] == '<':
                ns = s[1:len(s)]
                sections.append(Section(ns))
                continue
            if s == ">":
                break
            r = s.split(':')
            sections[len(sections) - 1].add(Data(r[0], r[1]))

    return File(sections)


def write(file: File, path: str):
    w = open(normalize(path), 'w')
    w.write(KV_HEADER + KV_NEXTLINE + KV_NEXTLINE)
    for section in file.sections:
        w.write(KV_LAB + section.name + KV_NEXTLINE)
        for data in section.data:
            w.write(KV_TAB + data.key + KV_SEPARATOR + data.value + KV_NEXTLINE)
        w.write(KV_RAB + KV_NEXTLINE + KV_NEXTLINE)
    w.close()


def remove_comments(txt: str) -> str:
    r = txt.split("#")
    r[0] = r[0].strip()
    return r[0]


def normalize(path: str) -> str:
    length = len(path)
    if length > 4:
        if not path[length - 4:length] == FILENAME_EXTENSION:
            return path + FILENAME_EXTENSION
        else:
            return path
    elif path == FILENAME_EXTENSION:
        return path
    else:
        return path + FILENAME_EXTENSION
