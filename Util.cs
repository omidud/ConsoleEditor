using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEditor
{
    //For Editor.cs 2.4 and up
    public static class Util
    {
        public static string Center(string str, int width)
        {
            string outStr = "";

            if (width <= str.Length)
                return str;

            int length = str.Length;
            int padSize = (width / 2) - (length / 2);

            outStr = "".PadLeft(padSize, ' ') + str + "".PadRight(padSize, ' ');

            if (outStr.Length < width)
                outStr = outStr + " ";


            return outStr;
        }

        public static int getMaxStringLenght(ref List<string> buffer)
        {
            int size = 0;
            foreach (string str in buffer)
            {
                if (str.Length > size)
                {
                    size = str.Length;
                }
            }

            return size;
        }

        public static void FlushKeyboard()
        {
            while (Console.In.Peek() != -1)
                Console.In.Read();
        }
    }//end  class
}//end namespace
