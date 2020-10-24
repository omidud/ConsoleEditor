using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleEditor
{
    //For Editor.cs 2.4 and up
    //the confirmation box when esc, to save, or not save and cancel
    public static class ConfirmBox
    {
        public static bool Show(ref EditData data)
        {
            bool retorna = false;
            //Console.CursorVisible = true;
            Console.CursorLeft = 0;
            Console.CursorTop = data.buffer.Count + 2;

            //int size = getMaxStringLenght() + margin;
            //if (size < 80) size = 80;
            //Console.WriteLine("_".PadLeft(size, '_'));

            string disPlayname = "";

            if (File.Exists(data.currFilename))
            {
                FileInfo fInfo = new FileInfo(data.currFilename);
                disPlayname = fInfo.Name;
            }
            else
            {
                disPlayname = data.currFilename;
            }
            // disPlayname = disPlayname.PadLeft(47);

            // Console.ForegroundColor = ConsoleColor.Black;
            // Console.BackgroundColor = ConsoleColor.White;
            //ref. https://en.wikipedia.org/wiki/Box-drawing_character#Unix,_CP/M,_BBS

            string msgbox = @"
                ╔═════════════════════════════════════════════════╗
                ║                                                 ║
                ║       Do you want to save the changes?          ║
                ║" + Util.Center(disPlayname, 49)             + @"║
                ║                                                 ║
                ║  ╔══════════╗     ╔══════════╗    ╔══════════╗  ║
                ║  ║  [y]es   ║     ║   [N]o   ║    ║ [C]ancel ║  ║
                ║  ╚══════════╝     ╚══════════╝    ╚══════════╝  ║
                ║                                                 ║
                ╚═════════════════════════════════════════════════╝";

            Console.Clear();
            Console.Write(msgbox);

            bool continueAsking = true;

            while (continueAsking)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    
                    Console.WriteLine();
                    Console.Write("                Save as [" + data.currFilename + "]: ");
                    
                    Console.TreatControlCAsInput = false;
                    Console.CursorVisible = true;
                    string newFilename = Console.ReadLine();                                      

                    if(newFilename != "")
                    {
                        data.currFilename = newFilename;
                    }

                    FileIO.SaveFile(ref data);
                    continueAsking = false;
                    retorna = true;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    continueAsking = false;
                    retorna = true;
                }
                else if (keyInfo.Key == ConsoleKey.C)
                {
                    continueAsking = false;
                    retorna = false;
                }
                else
                    continueAsking = true;
            }
            return retorna;
        }
    }//end class
}//end namespace
