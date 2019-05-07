using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLine;

namespace MemoriaCau
{

    class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages")]
            public bool Verbose { get; set; }
            [Option('d', "dni", Required = true, HelpText = "DNI to calculate the corresponding option")]
            public int DNI { get; set; }


        }
        const int CACHE_SIZE_BYTES = 2048;
        const int CACHE_BLOCK_SIZE_BYTES = 16;
        const ReplacementAlgorithm REPLACE_ALG = ReplacementAlgorithm.FIFO;
        static void Main(string[] args)
        {
            int dni = -1;
            Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                if (o.Verbose)
                {
                    Console.WriteLine("TODO: Verbose");
                }
                dni = o.DNI;
            });

            if (dni == -1)
            {
                return;
            }

            int h = (int)Math.Truncate(dni / 333.0);
            int optIndex = h % 11;
            int m = h * 256;

            var option = ProblemOptions.getOption(optIndex);

            IUbicationAlgorithm<int> cache = null;
            switch (option.UbicationAlgorithm)
            {
                case UbicationAlgorithm.SetAssociative:
                    cache = new NWayCache<int>(CACHE_SIZE_BYTES, CACHE_BLOCK_SIZE_BYTES, option.Ways ?? throw new InvalidDataException("Ways must be a number"), REPLACE_ALG, option.WriteAlgorithm);
                    break;
                case UbicationAlgorithm.Associative:
                    throw new NotImplementedException();
                case UbicationAlgorithm.Direct:
                    throw new NotImplementedException();
            }

            var access = new List<AccessRow<int>>();


            for (int i = 0; i <= 127; i++)
            {
                access.Add(cache.Read(_getAdress('b', m, i), 0).AddElement(string.Format("b[{0}]", i)));
                access.Add(cache.Read(_getAdress('c', m, i), 0).AddElement(string.Format("c[{0}]", i)));
                access.Add(cache.Read(_getAdress('d', m, i), 0).AddElement(string.Format("d[{0}]", i)));
                access.Add(cache.Write(_getAdress('a', m, i), 0).AddElement(string.Format("a[{0}]", i)));

                access.Add(cache.Read(_getAdress('a', m, i), 0).AddElement(string.Format("a[{0}]", i)));
                access.Add(cache.Read(_getAdress('c', m, i), 0).AddElement(string.Format("c[{0}]", i)));
                access.Add(cache.Write(_getAdress('e', m, i), 0).AddElement(string.Format("e[{0}]", i)));
            }

#if DEBUG
            string mainPath = string.Format("{0}/cache_solution/", Environment.CurrentDirectory);
#else
            string mainPath = string.Format("{0}/cache_solution/", AppDomain.CurrentDomain.BaseDirectory);
#endif

            string tablePath = mainPath + "table.csv";
            string extraInfoPath = mainPath + "info.txt";
            FileInfo fileTable = new FileInfo(tablePath);
            FileInfo fileExtraInfo = new FileInfo(extraInfoPath);
            fileTable.Directory.Create();
            fileExtraInfo.Directory.Create();

            var sb = new StringBuilder();
            sb.AppendLine(access[0].getHeader());
            foreach (var a in access)
            {
                sb.AppendLine(a.ToString());
            }
            File.WriteAllText(fileTable.FullName, sb.ToString());

            sb.Clear();
            sb.AppendLine(string.Format("DNI: {0}", dni));
            sb.AppendLine(string.Format("H: 0x{0:X}", h));
            sb.AppendLine(string.Format("Inici Dades(m): 0x{0:X}", m));
            sb.AppendLine(string.Format("Opció {0}: {1}", optIndex + 1, option.ToString()));
            var distBits = cache.AdressDistribution;
            sb.AppendLine("Distribució dels bits (per ordre):");
            foreach(DictionaryEntry bits in distBits)
                sb.AppendLine(string.Format("{0}: {1} bits", bits.Key, bits.Value));
            File.WriteAllText(fileExtraInfo.FullName, sb.ToString());
        }


        private static UInt32 _getAdress(char letter, int init, int index)
        {
            return (UInt32)((letter - 'a') * 128 * 4 + init + index * 4);
        }
    }
}
