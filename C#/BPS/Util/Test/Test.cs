using System;
using System.Collections.Generic;

namespace BPS.Util.Test
{
    public class Test
    {
        //public const string path = "D:/Documentos/OneDrive/DESKTOP/MLang/bps-lib/";
        //public const string path = "C:/Users/Panificadora Larissa/OneDrive/DESKTOP/BPSLib/";
        private const string path = "../../../../../";
        private const string wf = "write_test";
        private const string rf = "read_test.bps";
        private const bool DEBUG = true;

        public static void Main()
        {
#pragma warning disable CS0162 // Código inacessível detectado
            if (!DEBUG) return;
#pragma warning restore CS0162 // Código inacessível detectado

            //ReadWrite_Test();
            //RemoveData_Test();
            //RemoveSection_Test();
            //FindSection_Test();
            //SectionExists_Test();

            //Console.Read();
            //System.Threading.Thread.Sleep(60 * 1000);
        }

#pragma warning disable IDE0051 // Remover membros privados não utilizados
        private static void ReadWrite_Test()
#pragma warning restore IDE0051 // Remover membros privados não utilizados
        {
            File bpsFile = BPSIO.Read(path + rf);

            foreach (var s in bpsFile.AllSections())
            {
                Console.WriteLine(s.Name);

                foreach (var d in s.AllData())
                {
                    if (d.Value is List<object>)
                    {
                        Console.Write(d.Key + ":[");
                        foreach (var v in (List<object>)d.Value)
                        {
                            if (v is char)
                            {
                                Console.Write("'" + v + "',");
                            }
                        }
                        Console.WriteLine("]");
                    }
                    else
                        Console.WriteLine(d.Key + ":" + d.Value);
                }
                Console.WriteLine("\n");
            }

            BPSIO.Write(bpsFile, path + wf);
        }

#pragma warning disable IDE0051 // Remover membros privados não utilizados
        private static void RemoveData_Test()
#pragma warning restore IDE0051 // Remover membros privados não utilizados
        {
            File bpsFile = BPSIO.Read(path + rf);

            bpsFile.FindSection("section").RemoveData("key");

            BPSIO.Write(bpsFile, path + wf);
        }

#pragma warning disable IDE0051 // Remover membros privados não utilizados
        private static void RemoveSection_Test()
#pragma warning restore IDE0051 // Remover membros privados não utilizados
        {
            File bpsFile = BPSIO.Read(path + rf);

            Console.WriteLine(bpsFile.RemoveSection("section"));

            BPSIO.Write(bpsFile, path + wf);
        }

#pragma warning disable IDE0051 // Remover membros privados não utilizados
        private static void SectionExists_Test()
#pragma warning restore IDE0051 // Remover membros privados não utilizados
        {
            File bpsFile = BPSIO.Read(path + rf);

            Console.WriteLine(bpsFile.SectionExists("section"));

            BPSIO.Write(bpsFile, path + wf);
        }

#pragma warning disable IDE0051 // Remover membros privados não utilizados
        private static void FindSection_Test()
#pragma warning restore IDE0051 // Remover membros privados não utilizados
        {
            File bpsFile = BPSIO.Read(path + rf);

            Console.WriteLine(bpsFile.FindSection("section").Name);
            foreach (var d in bpsFile.FindSection("section").AllData())
            {
                Console.WriteLine(d.Key + ":" + d.Value);
            }

            BPSIO.Write(bpsFile, path + wf);
        }
    }
}
