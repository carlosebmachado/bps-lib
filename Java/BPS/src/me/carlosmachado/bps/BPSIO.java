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

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;
import java.util.ArrayList;

/**
 *
 * @author Carlos Eduardo de Borba Machado
 */
public class BPSIO {

    private final static String ERR_OPEN_NEW_WOUT_CLOSE_PREV
            = "Trying to open a new section without closing the previous one.";
    private final static String ERR_CLOSE_WOUT_OPEN_PREV
            = "Section was closed without open it previously.";

    private final static String KV_HEADER = "# BPS File";
    private final static String KV_NEXTLINE = "\n";
    private final static String KV_LAB = "<";
    private final static String KV_RAB = ">";
    private final static String KV_TAB = "    ";
    private final static String KV_SEPARATOR = ":";

    private final static String FILENAME_EXTENSION = ".bps";

    public static File read(String path) throws IOException, Exception {
        List<String> data;
        File file;

        try {
            data = lexicalAnalysis(normalize(path));
        } catch (IOException ex) {
            throw ex;
        }

        try {
            file = parser(data);
        } catch (Exception ex) {
            throw ex;
        }

        return file;
    }

    public static void write(File file, String path) throws IOException {
        try {
            var bw = new BufferedWriter(new FileWriter(normalize(path)));

            bw.append(KV_HEADER + KV_NEXTLINE + KV_NEXTLINE);

            for (Section section : file.findAll()) {
                bw.append(KV_LAB + section.name + KV_NEXTLINE);
                for (Data data : section.findAll()) {
                    bw.append(KV_TAB + data.key + KV_SEPARATOR + data.Value + KV_NEXTLINE);
                }
                bw.append(KV_RAB + KV_NEXTLINE + KV_NEXTLINE);
            }
            bw.close();
        } catch (IOException ex) {
            throw ex;
        }
    }

    private static List<String> lexicalAnalysis(String path) throws IOException {
        try {
            List<String> data = new ArrayList<>();
            String line = "";

            var file = new BufferedReader(new FileReader(path));

            while (true) {
                line = file.readLine();

                if (line == null) {
                    break;
                }

                // Ignora as linhas vazias
                if (line.equals("")) {
                    continue;
                }
                // Remove espaços em branco
                if (line.charAt(0) == ' ') {
                    line = line.trim();
                }
                // Caso ainda haja comentários, ignora
                if (line.charAt(0) == '#') {
                    continue;
                }
                // Ignora as linhas vazias
                if (line.equals("")) {
                    continue;
                }
                // Remove comentários
                line = removeComments(line);
                // Caso houvesse linhas com apenas comentários, ignora
                if (line.equals("")) {
                    continue;
                }
                // Remove espaços em branco
                if (line.charAt(0) == ' ') {
                    line = line.trim();
                }
                // Caso ainda haja comentários, ignora
                if (line.charAt(0) == '#') {
                    continue;
                }
                // Caso ainda sobre alguma linha vazia
                line = line.trim();
                if (line.equals("")) {
                    continue;
                }
                // Se a linha passou por todas as verificações é adicionada às linhas válidas
                data.add(line);
            }
            file.close();
            return data;
        } catch (IOException ex) {
            throw ex;
        }
    }

    private static File parser(List<String> data) throws Exception {
        List<List<String>> rawSections = new ArrayList<List<String>>();
        List<Section> sections = new ArrayList<>();
        boolean open = false;

        // Procura nos dados brutos, as seções
        for (var d : data) {
            // Encontrou a tag de abrir seção
            if (d.startsWith("<")) {
                // Se encontra uma tag de abrir seção mas ela já foi aberta
                if (open) {
                    throw new Exception(ERR_OPEN_NEW_WOUT_CLOSE_PREV);
                } else {
                    open = true;
                }
            }
            // Encontrou a tag de fechar seção
            if (d.equals(">")) {
                // Se encontra uma tag de fechar seção mas ela não foi aberta
                if (!open) {
                    throw new Exception(ERR_CLOSE_WOUT_OPEN_PREV);
                } else {
                    rawSections.add(new ArrayList<>());
                    open = false;
                }
            }

        }

        // Loop para cada seção encontrada anteriormente
        for (int i = 0; i < rawSections.size(); i++) {
            // Passa todas as linhas cruas para o rawSection
            while (true) {
                String curLine = data.get(0);
                data.remove(0);
                rawSections.get(i).add(curLine);
                if (curLine.equals(">")) {
                    break;
                }
            }
        }

        // Percorre rawSections criando as variáveis
        for (var sec : rawSections) {
            // Remove qualquer linha que não começe com '<'
            // PROVAVELMENTE SERÁ REMOVIDA
            while (!(sec.get(0).charAt(0) == '<')) {
                sec.remove(0);
            }
            for (String s : sec) {
                // Se estiver abrindo a seção
                if (s.charAt(0) == '<') {
                    String newS = s.substring(1, s.length());
                    sections.add(new Section(newS));
                    continue;
                }
                // Encontrou o fim da seção
                if (s.equals(">")) {
                    break;
                }
                // Senão entrou em nenhum if anterior, significa que é uma key/value e será adicionada a seção
                // Divide pelo ':'
                var r = s.split(":");
                // Cria um novo dado com key e data
                sections.get(sections.size() - 1).add(new Data(r[0], r[1]));
            }
        }

        return new File(sections);
    }

    private static String removeComments(String str) {
        // Divide e retorna apenas a parte esquerda da linha
        // A parte direita é apenas comentário
        var r = str.split("#");
        r[0] = r[0].trim();
        return r[0];
    }

    static String normalize(String path) {
        int length = path.length();
        if (length > 4) {
            if (!path.substring(length - 4, length).equals(FILENAME_EXTENSION)) {
                return path + FILENAME_EXTENSION;
            } else {
                return path;
            }
        } else if (path.equals(FILENAME_EXTENSION)) {
            return path;
        } else {
            return path + FILENAME_EXTENSION;
        }
    }

}
