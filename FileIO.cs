using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleEditor
{
    //For Editor.cs 2.4 and up
    // work the save and open file
    public static class FileIO
    {
        public static void OpenFile(string filename, ref List<string> buffer)
        {
            string[] arrString = File.ReadAllLines(filename);

            foreach (string str in arrString)
            {
                buffer.Add(str.Replace(Environment.NewLine, "")); //important remove the newlines
            }
        }

        public static void SaveFile(string filename, ref List<string> buffer)
        {
            string contents = "";
            int count = 0;

            foreach (string strLine in buffer)
            {
                count++;
                if (count < buffer.Count)
                    contents += strLine + Environment.NewLine; ////important add the newlines
                else
                {
                    if (strLine == "")
                        contents += strLine + Environment.NewLine;
                    else
                        contents += strLine; //no in the last line
                }
            }

            File.WriteAllText(filename, contents);

        }

    }//end class
}//end namespace
