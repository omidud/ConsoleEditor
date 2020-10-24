using System;
using System.Collections.Generic;

namespace ConsoleEditor
{    
    //this class is a helper, to clean the Editor.cs code
    //work the key inputs of the user    
    public static class KeyInput
    {
        public static void Enter(ref List<string> buffer, ref Point bufferPosition, ref int margin, ref int padding)
        {
            if (bufferPosition.Y == buffer.Count)
            {
                buffer.Add("");
            }

            string enterData = buffer[bufferPosition.Y];
            enterData = enterData.Substring(bufferPosition.X, enterData.Length - bufferPosition.X);
            buffer[bufferPosition.Y] = buffer[bufferPosition.Y].Substring(0, bufferPosition.X);
            buffer.Insert(bufferPosition.Y + 1, enterData);
            Screen.Refresh(ref buffer, ref padding, ref margin);
            bufferPosition.X = 0;
            Console.CursorLeft = bufferPosition.X + margin;
            bufferPosition.Y++;
            Console.CursorTop = bufferPosition.Y;
        }

        public static void BackSpace(ref List<string> buffer, ref Point bufferPosition, ref int margin, ref int padding) 
        {
            if (bufferPosition.X > 0)
            {
                if (bufferPosition.X < buffer[bufferPosition.Y].Length)
                {
                    string leftPart = buffer[bufferPosition.Y].Substring(0, bufferPosition.X - 1);
                    string rightPart = buffer[bufferPosition.Y].Substring(bufferPosition.X, buffer[bufferPosition.Y].Length - bufferPosition.X);
                    buffer[bufferPosition.Y] = leftPart + rightPart;
                }
                else
                {
                    string backLine = buffer[bufferPosition.Y].Substring(0, buffer[bufferPosition.Y].Length - 1);
                    buffer[bufferPosition.Y] = backLine;
                }
                bufferPosition.X--;
                Screen.WriteSentence(buffer[bufferPosition.Y] + " ", ref bufferPosition, ref margin);
            }
            else if (bufferPosition.X == 0)
            {

                if (buffer.Count == 0)
                    return;

                if (bufferPosition.Y == buffer.Count)
                {
                    bufferPosition.Y--;
                    return;
                }


                if (buffer[bufferPosition.Y] == null)
                    return;

                string temp = buffer[bufferPosition.Y];
                buffer.RemoveAt(bufferPosition.Y);

                if (bufferPosition.Y > 0)
                {
                    bufferPosition.X = buffer[bufferPosition.Y - 1].Length;
                    buffer[bufferPosition.Y - 1] = buffer[bufferPosition.Y - 1] + temp;
                    Screen.Refresh(ref buffer,ref padding, ref margin);
                    Console.CursorLeft = bufferPosition.X + margin;
                    bufferPosition.Y--;
                    Console.CursorTop = bufferPosition.Y;
                }
            }
        }

        public static void UpArrow(ref List<string> buffer, ref Point bufferPosition, ref int margin)
        {
            if (bufferPosition.Y > 0)
            {
                if (bufferPosition.X > buffer[bufferPosition.Y - 1].Length)
                {
                    bufferPosition.X = buffer[bufferPosition.Y - 1].Length;
                    Console.CursorLeft = bufferPosition.X + margin;
                }
                bufferPosition.Y--;
                Console.CursorTop = bufferPosition.Y;
            }
        }

        public static void DownArrow(ref List<string> buffer, ref Point bufferPosition, ref int margin)
        {
            if (bufferPosition.Y < buffer.Count)
            {
                if (bufferPosition.Y + 1 < buffer.Count)
                {
                    if (bufferPosition.X > buffer[bufferPosition.Y + 1].Length)
                    {
                        bufferPosition.X = buffer[bufferPosition.Y + 1].Length;
                        Console.CursorLeft = bufferPosition.X + margin;
                    }
                    bufferPosition.Y++;
                }
                Console.CursorTop = bufferPosition.Y;
            }
        }

        public static void RightArrow(ref List<string> buffer, ref Point bufferPosition, ref int margin )
        {
            if(buffer.Count > 0)
            {
                if (bufferPosition.X < buffer[bufferPosition.Y].Length)
                {
                    bufferPosition.X++;
                    Console.CursorLeft = bufferPosition.X + margin;
                }
            }
           
        }

        public static void LeftArrow(ref Point bufferPosition, ref int margin)
        {
            if (bufferPosition.X > 0)
            {
                bufferPosition.X--;
                Console.CursorLeft = bufferPosition.X + margin;
            }
        }

        public static void Home(ref Point bufferPosition, ref int margin)
        {
            bufferPosition.X = 0;
            Console.CursorLeft = bufferPosition.X + margin;
        }

        public static void End(ref List<string> buffer, ref Point bufferPosition, ref int margin)
        {
            if(buffer.Count > 0)
            {
                bufferPosition.X = buffer[bufferPosition.Y].Length;
                Console.CursorLeft = bufferPosition.X + margin; //put the cursor on the end of the string
            }
            
        }

        public static void Delete(ref List<string> buffer, ref Point bufferPosition, ref int margin, ref int padding)
        {
            if (buffer.Count == 0)
                return;

            if (bufferPosition.Y == buffer.Count)
                return;

            if (bufferPosition.Y == buffer.Count - 1)
            {

                if (bufferPosition.X < buffer[bufferPosition.Y].Length)
                {
                    buffer[bufferPosition.Y] = buffer[bufferPosition.Y].Remove(bufferPosition.X, 1);
                    Screen.WriteSentence(buffer[bufferPosition.Y] + " ", ref bufferPosition, ref margin);
                }
                return;
            }

            if (bufferPosition.Y >= 0)
            {
                if (buffer[bufferPosition.Y] == "") //cursor is in a empty line and user press DEL
                {
                    KeyInput.BackSpace(ref buffer, ref bufferPosition, ref margin, ref padding);
                    bufferPosition.Y++;
                    bufferPosition.X = 0;
                    Console.CursorTop = bufferPosition.Y;
                    Console.CursorLeft = bufferPosition.X + margin;
                    return;
                }

                if (bufferPosition.X == buffer[bufferPosition.Y].Length) //cursor is in the end of the line and user press DEL
                {
                    bufferPosition.Y++;
                    bufferPosition.X = 0;                    
                    KeyInput.BackSpace(ref buffer, ref bufferPosition, ref margin, ref padding);
                    return;
                }

                if (bufferPosition.X < buffer[bufferPosition.Y].Length)
                {
                    buffer[bufferPosition.Y] = buffer[bufferPosition.Y].Remove(bufferPosition.X, 1);
                    Screen.WriteSentence(buffer[bufferPosition.Y] + " ", ref bufferPosition, ref margin);
                }
            }
        }

        public static void Escape(ref List<string> buffer, ref string currFilename,ref bool running, ref int margin, ref int padding)
        {            
            if (ConfirmBox.Show(ref buffer, currFilename))
            {
                running = false;
            }
            else
            {
                Screen.Refresh(ref buffer, ref padding, ref margin);
                running = true;
            }
        }

        public static void Default(ref List<string> buffer, ref Point bufferPosition, ref int margin, ref ConsoleKeyInfo keyInfo)
        {
            if (buffer.Count == 0)
                buffer.Add("");

            if (buffer[bufferPosition.Y] == null)
                return;

            if (bufferPosition.X <= buffer[bufferPosition.Y].Length)
            {
                buffer[bufferPosition.Y] = buffer[bufferPosition.Y].Insert(bufferPosition.X, keyInfo.KeyChar.ToString());
                bufferPosition.X++;
                Screen.WriteSentence(buffer[bufferPosition.Y], ref bufferPosition, ref margin);
            }
        }

        public static void PageDown(ref List<string> buffer, ref Point bufferPosition, ref int margin)
        {
            int pageHeight = Console.WindowHeight;

            if (bufferPosition.Y < buffer.Count)
            {
                if (bufferPosition.Y + pageHeight < buffer.Count)
                {
                    if (bufferPosition.X > buffer[bufferPosition.Y + pageHeight].Length)
                    {
                        bufferPosition.X = buffer[bufferPosition.Y + pageHeight].Length;
                        Console.CursorLeft = bufferPosition.X + margin;
                    }
                    bufferPosition.Y = bufferPosition.Y + pageHeight;
                }
                else
                {
                    bufferPosition.Y = buffer.Count - 1;

                    if (bufferPosition.X > buffer[bufferPosition.Y].Length)
                    {
                        bufferPosition.X = buffer[bufferPosition.Y].Length;
                        Console.CursorLeft = bufferPosition.X + margin;
                    }                   
                }

                Console.CursorTop = bufferPosition.Y;
            }
        }

        public static void PageUp(ref List<string> buffer, ref Point bufferPosition, ref int margin)
        {
            int pageHeight = Console.WindowHeight;          

            if (bufferPosition.Y > 0)
            {
                if (bufferPosition.Y - pageHeight < 0)
                {
                    if (bufferPosition.X > buffer[0].Length)
                    {
                        bufferPosition.X = buffer[0].Length;
                        Console.CursorLeft = bufferPosition.X + margin;
                    }
                    bufferPosition.Y = 0;
                }
                else
                {
                    if (bufferPosition.X > buffer[bufferPosition.Y - pageHeight].Length)
                    {
                        bufferPosition.X = buffer[bufferPosition.Y - pageHeight].Length;
                        Console.CursorLeft = bufferPosition.X + margin;
                    }
                    bufferPosition.Y = bufferPosition.Y - pageHeight;
                }
                Console.CursorTop = bufferPosition.Y;
            }
        }
        
        public static void CtrlHome(ref Point bufferPosition, ref int margin)        
        {
            bufferPosition.Y = 0;
            bufferPosition.X = 0;
            Console.CursorLeft = bufferPosition.X + margin;
            Console.CursorTop = bufferPosition.Y;
        }

        public static void CtrlEnd(ref List<string> buffer, ref Point bufferPosition, ref int margin)
        {
            bufferPosition.Y = buffer.Count - 1;
            bufferPosition.X = buffer[bufferPosition.Y].Length;
            Console.CursorLeft = bufferPosition.X + margin;
            Console.CursorTop = bufferPosition.Y;
        }



    }//end class
}//end namespace
