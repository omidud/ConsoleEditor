using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleEditor
{
    //For Editor.cs 2.4 and up
    //this class is a helper, to clean the Editor.cs code
    //work the key inputs of the user    
    public static class KeyInput
    {
        public static void Enter(ref List<string> buffer, ref int X, ref int currLineY, ref int margin, ref int paddingSize)
        {
            if (currLineY == buffer.Count)
            {
                buffer.Add("");
            }

            string enterData = buffer[currLineY];
            enterData = enterData.Substring(X, enterData.Length - X);
            buffer[currLineY] = buffer[currLineY].Substring(0, X);
            buffer.Insert(currLineY + 1, enterData);
            Screen.Refresh(ref buffer, ref paddingSize, ref margin);
            X = 0;
            Console.CursorLeft = X + margin;
            currLineY++;
            Console.CursorTop = currLineY;
        }

        public static void BackSpace(ref List<string> buffer, ref int X, ref int currLineY, ref int margin, ref int paddingSize) 
        {
            if (X > 0)
            {
                if (X < buffer[currLineY].Length)
                {
                    string leftPart = buffer[currLineY].Substring(0, X - 1);
                    string rightPart = buffer[currLineY].Substring(X, buffer[currLineY].Length - X);
                    buffer[currLineY] = leftPart + rightPart;
                }
                else
                {
                    string backLine = buffer[currLineY].Substring(0, buffer[currLineY].Length - 1);
                    buffer[currLineY] = backLine;
                }
                X--;
                Screen.WriteSentence(buffer[currLineY] + " ", ref currLineY,ref margin,ref X);
            }
            else if (X == 0)
            {

                if (buffer.Count == 0)
                    return;

                if (currLineY == buffer.Count)
                {
                    currLineY--;
                    return;
                }


                if (buffer[currLineY] == null)
                    return;

                string temp = buffer[currLineY];
                buffer.RemoveAt(currLineY);

                if (currLineY > 0)
                {
                    X = buffer[currLineY - 1].Length;
                    buffer[currLineY - 1] = buffer[currLineY - 1] + temp;
                    Screen.Refresh(ref buffer,ref paddingSize, ref margin);
                    Console.CursorLeft = X + margin;
                    currLineY--;
                    Console.CursorTop = currLineY;
                }
            }
        }

        public static void UpArrow(ref List<string> buffer, ref int X, ref int currLineY, ref int margin)
        {
            if (currLineY > 0)
            {
                if (X > buffer[currLineY - 1].Length)
                {
                    X = buffer[currLineY - 1].Length;
                    Console.CursorLeft = X + margin;
                }
                currLineY--;
                Console.CursorTop = currLineY;
            }
        }

        public static void DownArrow(ref List<string> buffer, ref int X, ref int currLineY, ref int margin)
        {
            if (currLineY < buffer.Count)
            {
                if (currLineY + 1 < buffer.Count)
                {
                    if (X > buffer[currLineY + 1].Length)
                    {
                        X = buffer[currLineY + 1].Length;
                        Console.CursorLeft = X + margin;
                    }
                    currLineY++;
                }
                Console.CursorTop = currLineY;
            }
        }

        public static void RightArrow(ref List<string> buffer, ref int X, ref int currLineY, ref int margin )
        {
            if(buffer.Count > 0)
            {
                if (X < buffer[currLineY].Length)
                {
                    X++;
                    Console.CursorLeft = X + margin;
                }
            }
           
        }

        public static void LeftArrow(ref int X, ref int margin)
        {
            if (X > 0)
            {
                X--;
                Console.CursorLeft = X + margin;
            }
        }

        public static void Home(ref int X, ref int margin)
        {
            X = 0;
            Console.CursorLeft = X + margin;
        }

        public static void End(ref List<string> buffer, ref int X, ref int currLineY, ref int margin)
        {
            if(buffer.Count > 0)
            {
                X = buffer[currLineY].Length;
                Console.CursorLeft = X + margin; //put the cursor on the end of the string
            }
            
        }

        public static void Delete(ref List<string> buffer, ref int X, ref int currLineY, ref int margin, ref int paddingSize)
        {
            if (buffer.Count == 0)
                return;

            if (currLineY == buffer.Count)
                return;

            if (currLineY == buffer.Count - 1)
            {

                if (X < buffer[currLineY].Length)
                {
                    buffer[currLineY] = buffer[currLineY].Remove(X, 1);
                    Screen.WriteSentence(buffer[currLineY] + " ", ref currLineY, ref margin, ref X);
                }
                return;
            }

            if (currLineY >= 0)
            {
                if (buffer[currLineY] == "") //cursor is in a empty line and user press DEL
                {
                    KeyInput.BackSpace(ref buffer, ref X, ref currLineY, ref margin, ref paddingSize);
                    currLineY++;
                    X = 0;
                    Console.CursorTop = currLineY;
                    Console.CursorLeft = X + margin;
                    return;
                }

                if (X == buffer[currLineY].Length) //cursor is in the end of the line and user press DEL
                {
                    currLineY++;
                    X = 0;                    
                    KeyInput.BackSpace(ref buffer, ref X, ref currLineY, ref margin, ref paddingSize);
                    return;
                }

                if (X < buffer[currLineY].Length)
                {
                    buffer[currLineY] = buffer[currLineY].Remove(X, 1);
                    Screen.WriteSentence(buffer[currLineY] + " ", ref currLineY, ref margin, ref X);
                }
            }
        }

        public static void Escape(ref List<string> buffer, ref string currFilename,ref bool running, ref int margin, ref int paddingSize)
        {            
            if (ConfirmBox.Show(ref buffer, currFilename))
            {
                running = false;
            }
            else
            {
                Screen.Refresh(ref buffer, ref paddingSize, ref margin);
                running = true;
            }
        }

        public static void Default(ref List<string> buffer, ref int X, ref int currLineY,ref int margin, ref ConsoleKeyInfo keyInfo)
        {
            if (buffer.Count == 0)
                buffer.Add("");

            if (buffer[currLineY] == null)
                return;

            if (X <= buffer[currLineY].Length)
            {
                buffer[currLineY] = buffer[currLineY].Insert(X, keyInfo.KeyChar.ToString());
                X++;
                Screen.WriteSentence(buffer[currLineY], ref currLineY, ref margin, ref X);
            }
        }

        public static void PageDown(ref List<string> buffer, ref int X, ref int currLineY, ref int margin)
        {
            int pageHeight = Console.WindowHeight;
           // int lastPageHeight = buffer.Count % Console.WindowHeight;


            if (currLineY < buffer.Count)
            {
                if (currLineY + pageHeight < buffer.Count)
                {
                    if (X > buffer[currLineY + pageHeight].Length)
                    {
                        X = buffer[currLineY + pageHeight].Length;
                        Console.CursorLeft = X + margin;
                    }
                    currLineY = currLineY + pageHeight;
                }
                else
                {
                    currLineY = buffer.Count - 1;

                    if (X > buffer[currLineY].Length)
                    {
                        X = buffer[currLineY].Length;
                        Console.CursorLeft = X + margin;
                    }


                    //if (currLineY + lastPageHeight <= buffer.Count)
                    //{
                    //    if (X > buffer[currLineY + lastPageHeight - 1].Length)
                    //    {
                    //        X = buffer[currLineY + lastPageHeight - 1].Length;
                    //        Console.CursorLeft = X + margin;
                    //    }
                    //    currLineY = currLineY + lastPageHeight - 1;
                    //}
                }

                Console.CursorTop = currLineY;
            }
        }

        public static void PageUp(ref List<string> buffer, ref int X, ref int currLineY, ref int margin)
        {
            int pageHeight = Console.WindowHeight;
           // int lastPageHeight = buffer.Count % Console.WindowHeight;

            if (currLineY > 0)
            {
                if (currLineY - pageHeight < 0)
                {
                    if (X > buffer[0].Length)
                    {
                        X = buffer[0].Length;
                        Console.CursorLeft = X + margin;
                    }
                    currLineY = 0;
                }
                else
                {
                    if (X > buffer[currLineY - pageHeight].Length)
                    {
                        X = buffer[currLineY - pageHeight].Length;
                        Console.CursorLeft = X + margin;
                    }
                    currLineY = currLineY - pageHeight;
                }


                Console.CursorTop = currLineY;
            }
        }

        
        public static void CtrlHome(ref int X, ref int currLineY, ref int margin)        
        {
            currLineY = 0;
            X = 0;
            Console.CursorLeft = X + margin;
            Console.CursorTop = currLineY;
        }

        public static void CtrlEnd(ref List<string> buffer, ref int X, ref int currLineY, ref int margin)
        {
            currLineY = buffer.Count - 1;
            X = buffer[currLineY].Length;
            Console.CursorLeft = X + margin;
            Console.CursorTop = currLineY;
        }



    }//end class
}//end namespace
