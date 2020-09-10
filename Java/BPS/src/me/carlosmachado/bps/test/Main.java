package me.carlosmachado.bps.test;

import me.carlosmachado.bps.*;

class Main {

    public static final String path = "D:/Documentos/OneDrive/DESKTOP/BPSLib/";
    public static final String wf = "write_test";
    public static final String rf = "read_test.bps";
    
    public static void main(String[] args) throws Exception {
        //readWrite_Test();
        //removeData_Test();
        //removeSection_Test();
        //findSection_Test();
        //sectionExists_Test();

        //System.out.println("Hello world!");
    }

    public static void readWrite_Test() throws Exception {
        File bpsFile = BPSIO.read(path+rf);
 
        for (var s : bpsFile.findAll()) {
            System.out.println(s.name);

            for (var d : s.findAll()) {
                System.out.println(d.key + ":" + d.Value);
            }
            System.out.println("\n");
        }

        BPSIO.write(bpsFile, path+wf);
    }

    public static void removeData_Test() throws Exception {
        File bpsFile = BPSIO.read(path+rf);

        bpsFile.find("section").remove("key");

        BPSIO.write(bpsFile, path+wf);
    }

    public static void removeSection_Test() throws Exception {
        File bpsFile = BPSIO.read(path+rf);

        System.out.println(bpsFile.remove("section"));

        BPSIO.write(bpsFile, path+wf);
    }

    public static void sectionExists_Test() throws Exception {
        File bpsFile = BPSIO.read(path+rf);

        System.out.println(bpsFile.exists("section"));

        BPSIO.write(bpsFile, path+wf);
    }

    public static void findSection_Test() throws Exception {
        File bpsFile = BPSIO.read(path+rf);

        System.out.println(bpsFile.find("section").name);
        for (var d : bpsFile.find("section").findAll()) {
            System.out.println(d.key + ":" + d.Value);
        }

        BPSIO.write(bpsFile, path+wf);
    }
}
