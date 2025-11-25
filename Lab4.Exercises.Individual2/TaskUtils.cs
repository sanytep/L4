using System.Text;

namespace Lab4.Exercises.Individual2
{
    static class TaskUtils
    {
        public static bool RemoveComments(string line, out string newLine, ref bool insideComment)
        {
            newLine = line;
            bool foundComment = false;

            if (insideComment)
            {
                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] == '*' && line[i + 1] == '/')
                    {
                        insideComment = false;
                        line = line.Substring(i + 2);
                        foundComment = true;
                        break;
                    }
                }
                if (insideComment)
                {
                    newLine = "";
                    return true;
                }
            }

            StringBuilder result = new StringBuilder();
            bool insideString = false;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    insideString = !insideString;
                    result.Append(line[i]);
                    continue;
                }
                if (!insideString && line[i] == '/')
                {
                    if (line[i + 1] == '/')
                    {
                        foundComment = true;
                        break;
                    }

                    if (line[i + 1] == '*')
                    {
                        foundComment = true;
                        bool closingFound = false;
                        for (int j = i + 2; j < line.Length - 1; j++)
                        {
                            if (line[j] == '*' && line[j + 1] == '/')
                            {
                                i = j + 1;
                                closingFound = true;
                                break;
                            }
                        }

                        if(!closingFound)
                        {
                            insideComment = true;
                            break;
                        }

                        continue;
                    }
                }
                result.Append(line[i]);
            }

            newLine = result.ToString();
            return foundComment;
        }
    }
}
