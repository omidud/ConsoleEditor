﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleEditor
{
    //Editor 2.4
    public class Editor
    {
        private int currLineY = 0;
        private List<string> buffer = new List<string>();
        private int X = 0;
        private string currFilename;
        private int paddingSize = 0;
        private int margin = 0;

        public Editor() //no filename provided,  currFilename = "Untitled";
        {
            //new file Untitled
            currFilename = "Untitled";
            buffer.Add("");            
            Screen.Init(ref buffer, ref paddingSize, ref margin, ref X, ref currLineY);
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
            
            Screen.Init(ref buffer,ref paddingSize, ref margin, ref X,ref currLineY);                        
        }
               
        public void Run()
        {
            Console.TreatControlCAsInput = true;
            bool running = true;

            while (running)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;

                if (keyInfo.Modifiers == ConsoleModifiers.Control)
                {

                    switch(keyInfo.Key)
                    {
                        case ConsoleKey.Home:
                            KeyInput.CtrlHome(ref X, ref currLineY, ref margin);
                            break;
                        case ConsoleKey.End:
                            KeyInput.CtrlEnd(ref buffer, ref X, ref currLineY, ref margin);
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
                            KeyInput.UpArrow(ref buffer, ref X, ref currLineY, ref margin);
                            break;
                        case ConsoleKey.DownArrow:
                            KeyInput.DownArrow(ref buffer, ref X, ref currLineY, ref margin);                            
                            break;
                        case ConsoleKey.RightArrow:  
                            KeyInput.RightArrow(ref buffer, ref X, ref currLineY, ref margin);                            
                            break;
                        case ConsoleKey.LeftArrow:
                            KeyInput.LeftArrow(ref X, ref margin);
                            break;
                        case ConsoleKey.Home:
                            KeyInput.Home(ref X, ref margin);                            
                            break;
                        case ConsoleKey.End:
                            KeyInput.End(ref buffer, ref X, ref currLineY, ref margin);                            
                            break;                       
                        case ConsoleKey.Delete:
                            KeyInput.Delete(ref buffer, ref X, ref currLineY, ref margin, ref paddingSize);
                            break;
                        case ConsoleKey.Backspace:                                                       
                            KeyInput.BackSpace(ref buffer, ref X, ref currLineY, ref margin, ref paddingSize);
                            break;
                        case ConsoleKey.Enter:                            
                            KeyInput.Enter(ref buffer, ref X, ref currLineY, ref margin, ref paddingSize);
                            break;
                        case ConsoleKey.PageDown:
                            KeyInput.PageDown(ref buffer, ref X, ref currLineY, ref margin);
                            break;
                        case ConsoleKey.PageUp:
                            KeyInput.PageUp(ref buffer, ref X, ref currLineY, ref margin);
                            break;
                        case ConsoleKey.Escape:
                            KeyInput.Escape(ref buffer, ref currFilename, ref running, ref margin, ref paddingSize);                           
                            break;
                        default:
                            KeyInput.Default(ref buffer, ref X, ref currLineY, ref margin, ref keyInfo);                           
                            break;
                    }//end switch         
                }
                Console.Title = "Current Line: " + currLineY.ToString() + "  X: " + X.ToString();
                Console.CursorVisible = true;
            }//end while

            Console.Clear();
        }
                
    }//end class
}//end namespace
