using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises._4._2
{
    internal class InOut
    {
        public static int LongestLine(string fin)
        {
            int No = -1;
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                int length = 0;
                int lineNo = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > length)
                    {
                        length = line.Length;
                        No = lineNo;
                    }
                    lineNo++;
                }
            }
            return No;
        }

        public static void RemoveLine(string fin, string fout, int No)
        {
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                int lineNo = 0;
                using (var writer = File.CreateText(fout))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (No != lineNo)
                        {
                            writer.WriteLine(line);
                        }
                        lineNo++;
                    }
                }
            }
        }

    }
}
