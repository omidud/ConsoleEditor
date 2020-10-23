using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEditor
{
    //For Editor.cs 2.4 and up
    //another helper just for draw,clean, write in the screen
    //display purpose
    public static class Screen
    {
        public static void Init(ref List<string> buffer, ref int paddingSize, ref int margin,ref int X, ref int currLineY)
        {
            Screen.Refresh(ref buffer, ref paddingSize, ref margin);
            X = 0;
            Console.CursorLeft = X + margin;
            currLineY = 0;
            Console.CursorTop = currLineY;
        }

        public static void WriteSentence(string sentence, ref int currLineY, ref int margin, ref int X)
        {
            Console.CursorTop = currLineY;
            Console.CursorVisible = false;
            Console.CursorLeft = 0 + margin;
            Console.Write(sentence);            
            Console.CursorLeft = X + margin;
            Console.CursorVisible = true;
        }

        public static void Refresh(ref List<string> buffer, ref int paddingSize, ref int margin)
        {
            int totalLineNumber = buffer.Count;
            paddingSize = totalLineNumber.ToString().Length;
            margin = paddingSize + 2;

            Console.CursorVisible = false;
            Console.Clear();
            int lineNumber = 0;
            foreach (string texto in buffer)
            {                
                lineNumber++;
                Console.WriteLine(lineNumber.ToString().PadLeft(paddingSize, ' ') + ": " + texto);
            }
            Console.CursorVisible = true;
        }

    }//end class
}//end namespace
