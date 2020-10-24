using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleEditor
{
    //Editor 2.6
    public class Editor
    {        
        private EditData data;

        public Editor() //no filename provided,  currFilename = "Untitled";
        {
            data = new EditData();

            //new file Untitled
            data.currFilename = "Untitled";
            data.buffer.Add("");            
            Screen.Init(ref data);
        }

        public Editor(string filename)
        {
            data = new EditData();
            data.currFilename = filename;

            if(File.Exists(data.currFilename)) //if exist , open for edit
            {
                FileIO.OpenFile(ref data);
                if (data.buffer.Count == 0)
                    data.buffer.Add("");
            }
            else
            {
                //new file but with filename
                data.buffer.Add("");
            }            
            Screen.Init(ref data);
        }
               
        public void Run()
        {
            Console.Title = "Y: " + data.bufferPosition.Y.ToString() + "  X: " + data.bufferPosition.X.ToString();
            Console.TreatControlCAsInput = true;
            bool running = true;
            Console.CursorLeft = data.margin;

            while (running)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;

                if (keyInfo.Modifiers == ConsoleModifiers.Control)
                {

                    switch(keyInfo.Key)
                    {
                        case ConsoleKey.Home:
                            KeyInput.CtrlHome(ref data);
                            break;
                        case ConsoleKey.End:
                            KeyInput.CtrlEnd(ref data);
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
                            KeyInput.UpArrow(ref data);
                            break;
                        case ConsoleKey.DownArrow:
                            KeyInput.DownArrow(ref data);                            
                            break;
                        case ConsoleKey.RightArrow:  
                            KeyInput.RightArrow(ref data);                            
                            break;
                        case ConsoleKey.LeftArrow:
                            KeyInput.LeftArrow(ref data);
                            break;
                        case ConsoleKey.Home:
                            KeyInput.Home(ref data);                            
                            break;
                        case ConsoleKey.End:
                            KeyInput.End(ref data);                            
                            break;                       
                        case ConsoleKey.Delete:
                            KeyInput.Delete(ref data);
                            break;
                        case ConsoleKey.Backspace:                                                       
                            KeyInput.BackSpace(ref data);
                            break;
                        case ConsoleKey.Enter:                            
                            KeyInput.Enter(ref data);
                            break;
                        case ConsoleKey.PageDown:
                            KeyInput.PageDown(ref data);
                            break;
                        case ConsoleKey.PageUp:
                            KeyInput.PageUp(ref data);
                            break;
                        case ConsoleKey.Escape:
                            KeyInput.Escape(ref data, ref running);                           
                            break;
                        default:
                            KeyInput.Default(ref data, ref keyInfo);                           
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
