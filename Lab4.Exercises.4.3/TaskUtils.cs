using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Exercises._4._3
{
    class TaskUtils
    {
        /** Removes comments and returns an indication about performed activity.
        @param line – line having possible comments
        @param newLine – line without comments */
        public static bool RemoveComments(string line, out string newLine)
        {
            newLine = line;
            for (int i = 0; i < line.Length - 1; i++)
                if (line[i] == '/' && line[i + 1] == '/')
                {
                    newLine = line.Remove(i);
                    return true;
                }
            return false;
        }

    }
}
