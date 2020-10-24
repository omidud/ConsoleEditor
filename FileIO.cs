using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleEditor
{    
    // work the save and open file
    public static class FileIO
    {
        public static void OpenFile(ref EditData data)
        {
            string[] arrString = File.ReadAllLines(data.currFilename);

            foreach (string str in arrString)
            {
                data.buffer.Add(str.Replace(Environment.NewLine, "")); //important remove the newlines
            }
        }

        public static void SaveFile(ref EditData data)
        {
            string contents = "";
            int count = 0;

            foreach (string strLine in data.buffer)
            {
                count++;
                if (count < data.buffer.Count)
                    contents += strLine + Environment.NewLine; ////important add the newlines
                else
                {
                    if (strLine == "")
                        contents += strLine + Environment.NewLine;
                    else
                        contents += strLine; //no in the last line
                }                
            }

            File.WriteAllText(data.currFilename, contents);

            data.buffer.Clear();

            
        }

    }//end class
}//end namespace
