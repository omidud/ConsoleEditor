using System;
using System.Collections.Generic;

namespace ConsoleEditor
{    
    //this class is a helper, to clean the Editor.cs code
    //work the key inputs of the user    
    public static class KeyInput
    {
        public static void Enter(ref EditData data)
        {
            if (data.bufferPosition.Y == data.buffer.Count)
            {
                data.buffer.Add("");
            }

            string enterData = data.buffer[data.bufferPosition.Y];
            enterData = enterData.Substring(data.bufferPosition.X, enterData.Length - data.bufferPosition.X);
            data.buffer[data.bufferPosition.Y] = data.buffer[data.bufferPosition.Y].Substring(0, data.bufferPosition.X);
            data.buffer.Insert(data.bufferPosition.Y + 1, enterData);
            Screen.Refresh(ref data);
            data.bufferPosition.X = 0;
            Console.CursorLeft = data.bufferPosition.X + data.margin;
            data.bufferPosition.Y++;
            Console.CursorTop = data.bufferPosition.Y;
        }

        public static void BackSpace(ref EditData data) 
        {
            if (data.bufferPosition.X > 0)
            {
                if (data.bufferPosition.X < data.buffer[data.bufferPosition.Y].Length)
                {
                    string leftPart = data.buffer[data.bufferPosition.Y].Substring(0, data.bufferPosition.X - 1);
                    string rightPart = data.buffer[data.bufferPosition.Y].Substring(data.bufferPosition.X, data.buffer[data.bufferPosition.Y].Length - data.bufferPosition.X);
                    data.buffer[data.bufferPosition.Y] = leftPart + rightPart;
                }
                else
                {
                    string backLine = data.buffer[data.bufferPosition.Y].Substring(0, data.buffer[data.bufferPosition.Y].Length - 1);
                    data.buffer[data.bufferPosition.Y] = backLine;
                }
                data.bufferPosition.X--;
                Screen.WriteSentence(data.buffer[data.bufferPosition.Y] + " ", ref data);
            }
            else if (data.bufferPosition.X == 0)
            {

                if (data.buffer.Count == 0)
                    return;

                if (data.bufferPosition.Y == data.buffer.Count)
                {
                    data.bufferPosition.Y--;
                    return;
                }


                if (data.buffer[data.bufferPosition.Y] == null)
                    return;

                string temp = data.buffer[data.bufferPosition.Y];
                data.buffer.RemoveAt(data.bufferPosition.Y);

                if (data.bufferPosition.Y > 0)
                {
                    data.bufferPosition.X = data.buffer[data.bufferPosition.Y - 1].Length;
                    data.buffer[data.bufferPosition.Y - 1] = data.buffer[data.bufferPosition.Y - 1] + temp;
                    Screen.Refresh(ref data);
                    Console.CursorLeft = data.bufferPosition.X + data.margin;
                    data.bufferPosition.Y--;
                    Console.CursorTop = data.bufferPosition.Y;
                }
                else if(data.bufferPosition.Y == 0) ///verificar esto
                {
                    data.bufferPosition.X = data.buffer[0].Length;
                    data.buffer[0] = data.buffer[0] + temp;
                    Screen.Refresh(ref data);
                    Console.CursorLeft = data.bufferPosition.X + data.margin;
                    data.bufferPosition.Y = 0;
                    Console.CursorTop = data.bufferPosition.Y;
                }
            }
        }

        public static void UpArrow(ref EditData data)
        {
            if (data.bufferPosition.Y > 0)
            {
                if (data.bufferPosition.X > data.buffer[data.bufferPosition.Y - 1].Length)
                {
                    data.bufferPosition.X = data.buffer[data.bufferPosition.Y - 1].Length;
                    Console.CursorLeft = data.bufferPosition.X + data.margin;
                }
                data.bufferPosition.Y--;
                Console.CursorTop = data.bufferPosition.Y;
            }
        }

        public static void DownArrow(ref EditData data)
        {
            if (data.bufferPosition.Y < data.buffer.Count)
            {
                if (data.bufferPosition.Y + 1 < data.buffer.Count)
                {
                    if (data.bufferPosition.X > data.buffer[data.bufferPosition.Y + 1].Length)
                    {
                        data.bufferPosition.X = data.buffer[data.bufferPosition.Y + 1].Length;
                        Console.CursorLeft = data.bufferPosition.X + data.margin;
                    }
                    data.bufferPosition.Y++;
                }
                Console.CursorTop = data.bufferPosition.Y;
            }
        }

        public static void RightArrow(ref EditData data)
        {
            if(data.buffer.Count > 0)
            {
                if (data.bufferPosition.X < data.buffer[data.bufferPosition.Y].Length)
                {
                    data.bufferPosition.X++;
                    Console.CursorLeft = data.bufferPosition.X + data.margin;
                }
            }
           
        }

        public static void LeftArrow(ref EditData data)
        {
            if (data.bufferPosition.X > 0)
            {
                data.bufferPosition.X--;
                Console.CursorLeft = data.bufferPosition.X + data.margin;
            }
        }

        public static void Home(ref EditData data)
        {
            data.bufferPosition.X = 0;
            Console.CursorLeft = data.bufferPosition.X + data.margin;
        }

        public static void End(ref EditData data)
        {
            if(data.buffer.Count > 0)
            {
                data.bufferPosition.X = data.buffer[data.bufferPosition.Y].Length;
                Console.CursorLeft = data.bufferPosition.X + data.margin; //put the cursor on the end of the string
            }            
        }

        public static void Delete(ref EditData data)
        {
            if (data.buffer.Count == 0)
                return;

            if (data.bufferPosition.Y == data.buffer.Count)
                return;

            if (data.bufferPosition.Y == data.buffer.Count - 1)
            {

                if (data.bufferPosition.X < data.buffer[data.bufferPosition.Y].Length)
                {
                    data.buffer[data.bufferPosition.Y] = data.buffer[data.bufferPosition.Y].Remove(data.bufferPosition.X, 1);
                    Screen.WriteSentence(data.buffer[data.bufferPosition.Y] + " ", ref data);
                }
                return;
            }

            if (data.bufferPosition.Y >= 0)
            {
                if (data.buffer[data.bufferPosition.Y] == "") //cursor is in a empty line and user press DEL
                {
                    KeyInput.BackSpace(ref data);
                    data.bufferPosition.Y++;
                    data.bufferPosition.X = 0;
                    Console.CursorTop = data.bufferPosition.Y;
                    Console.CursorLeft = data.bufferPosition.X + data.margin;
                    return;
                }

                if (data.bufferPosition.X == data.buffer[data.bufferPosition.Y].Length) //cursor is in the end of the line and user press DEL
                {
                    data.bufferPosition.Y++;
                    data.bufferPosition.X = 0;                    
                    KeyInput.BackSpace(ref data);
                    return;
                }

                if (data.bufferPosition.X < data.buffer[data.bufferPosition.Y].Length)
                {
                    data.buffer[data.bufferPosition.Y] = data.buffer[data.bufferPosition.Y].Remove(data.bufferPosition.X, 1);
                    Screen.WriteSentence(data.buffer[data.bufferPosition.Y] + " ", ref data);
                }
            }
        }

        public static void Escape(ref EditData data, ref bool running)
        {            
            if (ConfirmBox.Show(ref data))
            {
                running = false;
            }
            else
            {
                Screen.Refresh(ref data);
                running = true;
            }
        }

        public static void Default(ref EditData data, ref ConsoleKeyInfo keyInfo)
        {
            if (data.buffer.Count == 0)
                data.buffer.Add("");

            if (data.buffer[data.bufferPosition.Y] == null)
                return;

            if (data.bufferPosition.X <= data.buffer[data.bufferPosition.Y].Length)
            {
                data.buffer[data.bufferPosition.Y] = data.buffer[data.bufferPosition.Y].Insert(data.bufferPosition.X, keyInfo.KeyChar.ToString());
                data.bufferPosition.X++;
                Screen.WriteSentence(data.buffer[data.bufferPosition.Y], ref data);
            }
        }

        public static void PageDown(ref EditData data)
        {
            int pageHeight = Console.WindowHeight;

            if (data.bufferPosition.Y < data.buffer.Count)
            {
                if (data.bufferPosition.Y + pageHeight < data.buffer.Count)
                {
                    if (data.bufferPosition.X > data.buffer[data.bufferPosition.Y + pageHeight].Length)
                    {
                        data.bufferPosition.X = data.buffer[data.bufferPosition.Y + pageHeight].Length;
                        Console.CursorLeft = data.bufferPosition.X + data.margin;
                    }
                    data.bufferPosition.Y = data.bufferPosition.Y + pageHeight;
                }
                else
                {
                    data.bufferPosition.Y = data.buffer.Count - 1;

                    if (data.bufferPosition.X > data.buffer[data.bufferPosition.Y].Length)
                    {
                        data.bufferPosition.X = data.buffer[data.bufferPosition.Y].Length;
                        Console.CursorLeft = data.bufferPosition.X + data.margin;
                    }                   
                }

                Console.CursorTop = data.bufferPosition.Y;
            }
        }

        public static void PageUp(ref EditData data)
        {
            int pageHeight = Console.WindowHeight;          

            if (data.bufferPosition.Y > 0)
            {
                if (data.bufferPosition.Y - pageHeight < 0)
                {
                    if (data.bufferPosition.X > data.buffer[0].Length)
                    {
                        data.bufferPosition.X = data.buffer[0].Length;
                        Console.CursorLeft = data.bufferPosition.X + data.margin;
                    }
                    data.bufferPosition.Y = 0;
                }
                else
                {
                    if (data.bufferPosition.X > data.buffer[data.bufferPosition.Y - pageHeight].Length)
                    {
                        data.bufferPosition.X = data.buffer[data.bufferPosition.Y - pageHeight].Length;
                        Console.CursorLeft = data.bufferPosition.X + data.margin;
                    }
                    data.bufferPosition.Y = data.bufferPosition.Y - pageHeight;
                }
                Console.CursorTop = data.bufferPosition.Y;
            }
        }
        
        public static void CtrlHome(ref EditData data)        
        {
            data.bufferPosition.Y = 0;
            data.bufferPosition.X = 0;
            Console.CursorLeft = data.bufferPosition.X + data.margin;
            Console.CursorTop = data.bufferPosition.Y;
        }

        public static void CtrlEnd(ref EditData data)
        {
            data.bufferPosition.Y = data.buffer.Count - 1;
            data.bufferPosition.X = data.buffer[data.bufferPosition.Y].Length;
            Console.CursorLeft = data.bufferPosition.X + data.margin;
            Console.CursorTop = data.bufferPosition.Y;
        }



    }//end class
}//end namespace
