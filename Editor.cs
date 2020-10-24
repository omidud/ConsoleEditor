using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleEditor
{
    //Editor 2.5
    public class Editor
    {
        private Point bufferPosition = new Point();
        private List<string> buffer = new List<string>();        
        private string currFilename;
        private int padding = 0;
        private int margin = 0;

        public Editor() //no filename provided,  currFilename = "Untitled";
        {
            //new file Untitled
            currFilename = "Untitled";           
            buffer.Add("");            
            Screen.Init(ref buffer, ref bufferPosition, ref margin, ref padding);
        }

        public Editor(string filename)
        {
            currFilename = filename;

            if(File.Exists(currFilename)) //if exist , open for edit
            {
                FileIO.OpenFile(currFilename, ref buffer);
                if (buffer.Count == 0)
                    buffer.Add("");
            }
            else
            {
                //new file but with filename
                buffer.Add("");
            }            
            Screen.Init(ref buffer, ref bufferPosition, ref margin, ref padding);
        }
               
        public void Run()
        {
            Console.Title = "Y: " + bufferPosition.Y.ToString() + "  X: " + bufferPosition.X.ToString();
            Console.TreatControlCAsInput = true;
            bool running = true;
            Console.CursorLeft = margin;

            while (running)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;

                if (keyInfo.Modifiers == ConsoleModifiers.Control)
                {

                    switch(keyInfo.Key)
                    {
                        case ConsoleKey.Home:
                            KeyInput.CtrlHome(ref bufferPosition, ref margin);
                            break;
                        case ConsoleKey.End:
                            KeyInput.CtrlEnd(ref buffer, ref bufferPosition, ref margin);
                            break;
                        case ConsoleKey.X:
                            //cut
                            break;
                        case ConsoleKey.C:
                            //copy
                            break;
                        case ConsoleKey.V:
                            //paste
                            break;

                        default:
                            break;
                    }
                   
                }
                else if (keyInfo.Modifiers == 0 || keyInfo.Modifiers == ConsoleModifiers.Shift)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            KeyInput.UpArrow(ref buffer, ref bufferPosition, ref margin);
                            break;
                        case ConsoleKey.DownArrow:
                            KeyInput.DownArrow(ref buffer, ref bufferPosition, ref margin);                            
                            break;
                        case ConsoleKey.RightArrow:  
                            KeyInput.RightArrow(ref buffer, ref bufferPosition, ref margin);                            
                            break;
                        case ConsoleKey.LeftArrow:
                            KeyInput.LeftArrow(ref bufferPosition, ref margin);
                            break;
                        case ConsoleKey.Home:
                            KeyInput.Home(ref bufferPosition, ref margin);                            
                            break;
                        case ConsoleKey.End:
                            KeyInput.End(ref buffer, ref bufferPosition, ref margin);                            
                            break;                       
                        case ConsoleKey.Delete:
                            KeyInput.Delete(ref buffer, ref bufferPosition, ref margin, ref padding);
                            break;
                        case ConsoleKey.Backspace:                                                       
                            KeyInput.BackSpace(ref buffer, ref bufferPosition, ref margin, ref padding);
                            break;
                        case ConsoleKey.Enter:                            
                            KeyInput.Enter(ref buffer, ref bufferPosition, ref margin, ref padding);
                            break;
                        case ConsoleKey.PageDown:
                            KeyInput.PageDown(ref buffer, ref bufferPosition, ref margin);
                            break;
                        case ConsoleKey.PageUp:
                            KeyInput.PageUp(ref buffer, ref bufferPosition, ref margin);
                            break;
                        case ConsoleKey.Escape:
                            KeyInput.Escape(ref buffer, ref currFilename, ref running, ref margin, ref padding);                           
                            break;
                        default:
                            KeyInput.Default(ref buffer, ref bufferPosition, ref margin, ref keyInfo);                           
                            break;
                    }//end switch         
                }
                //Console.Title = "Y: " + bufferPosition.Y.ToString() + "  X: " + bufferPosition.X.ToString();
                Console.CursorVisible = true;
            }//end while

            Console.Clear();
        }
                
    }//end class
}//end namespace
