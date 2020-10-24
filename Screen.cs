using System;
using System.Collections.Generic;

namespace ConsoleEditor
{    
    //another helper just for draw,clean, write in the screen
    //display purpose
    public static class Screen
    {    //ref buffer, ref bufferPosition, ref margin, ref padding
        public static void Init(ref List<string> buffer, ref Point bufferPoint, ref int margin, ref int padding)
        {           
            Screen.Refresh(ref buffer, ref margin, ref padding);            
            bufferPoint.X = 0;
            Console.CursorLeft = bufferPoint.X + margin;
            //Console.CursorLeft = bufferPoint.X + padding;
            bufferPoint.Y = 0;
            Console.CursorTop = bufferPoint.Y;
        }

        public static void WriteSentence(string sentence, ref Point bufferPoint, ref int margin)
        {
            Console.CursorTop = bufferPoint.Y;
            Console.CursorVisible = false;
            Console.CursorLeft = 0 + margin;
            Console.Write(sentence);            
            Console.CursorLeft = bufferPoint.X + margin;
            Console.CursorVisible = true;
        }

        public static void Refresh(ref List<string> buffer, ref int margin, ref int padding )
        {
            int width = Util.getMaxStringLenght(ref buffer);
            Console.WindowWidth = width + margin + 10;

            int totalLineNumber = buffer.Count;
            padding = totalLineNumber.ToString().Length; // = 3
            margin = padding + 2;                        // = 5
            
            Console.CursorVisible = false;
            Console.Clear();
            int lineNumber = 0;
            foreach (string texto in buffer)
            {                
                lineNumber++;
                Console.WriteLine(lineNumber.ToString().PadLeft(padding, ' ') + ": " + texto);
            }
            Console.CursorVisible = true;
            
        }

    }//end class
}//end namespace
