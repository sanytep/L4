using System.Text;
using System.IO;

namespace Lab4.Exercises.Individual2
{
    static class InOut
    {
        public static void Process(string fin, string fout, string finfo)
        {
            string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
            bool insideComment = false;
            using (var writerF = File.CreateText(fout))
            {
                using (var writerI = File.CreateText(finfo))
                {
                    foreach (string line in lines)
                    {
                        if (line.Length > 0)
                        {
                            string newLine = line;
                            if (TaskUtils.RemoveComments(line, out newLine, ref insideComment))
                            {
                                writerI.WriteLine(line);
                            }
                            if (newLine.Length > 0)
                            {
                                writerF.WriteLine(newLine);
                            }
                        }
                        else
                        {
                            writerF.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
