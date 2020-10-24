using System;
using System.Collections.Generic;

namespace ConsoleEditor
{    
    //another helper just for draw,clean, write in the screen
    //display purpose
    public static class Screen
    {           
        public static void Init(ref EditData data)
        {           
            Screen.Refresh(ref data);
            data.bufferPosition.X = 0;
            Console.CursorLeft = data.bufferPosition.X + data.margin;            
            data.bufferPosition.Y = 0;
            Console.CursorTop = data.bufferPosition.Y;
        }

        public static void WriteSentence(string sentence, ref EditData data)
        {
            Console.CursorTop = data.bufferPosition.Y;
            Console.CursorVisible = false;
            Console.CursorLeft = 0 + data.margin;
            Console.Write(sentence);            
            Console.CursorLeft = data.bufferPosition.X + data.margin;
            Console.CursorVisible = true;
        }

        public static void Refresh(ref EditData data)
        {
            int width = Util.getMaxStringLenght(ref data);
            Console.WindowWidth = width + data.margin + 10;

            int totalLineNumber = data.buffer.Count;
            data.padding = totalLineNumber.ToString().Length; // = 3
            data.margin = data.padding + 2;                        // = 5
            
            Console.CursorVisible = false;
            Console.Clear();
            int lineNumber = 0;
            foreach (string texto in data.buffer)
            {                
                lineNumber++;
                Console.WriteLine(lineNumber.ToString().PadLeft(data.padding, ' ') + ": " + texto);
            }
            Console.CursorLeft = data.bufferPosition.X + data.margin;
            Console.CursorTop = data.bufferPosition.Y;
            Console.CursorVisible = true;
            
        }

    }//end class
}//end namespace
