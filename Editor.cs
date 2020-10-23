using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleEditor
{
    //Editor2
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
            currFilename = "Untitled";
            buffer.Add("");
            Init();
        }             

        public Editor(string filename)
        {
            currFilename = filename;

            if(File.Exists(currFilename)) //if exist , open for edit
            {
                OpenFile(currFilename);
            }
            else
            {
                //blank filename but with filename name
                buffer.Add("");
            }
                
            Init();
        }


        private void OpenFile(string filename)
        {
            string[] arrString = File.ReadAllLines(filename);

            foreach (string str in arrString)
            {
                buffer.Add(str.Replace(Environment.NewLine, "")); //important remove the newlines
            }
        }

        private void SaveFile(string filename)
        {
            string contents = "";

            foreach (string strLine in buffer)
            {
                contents += strLine + Environment.NewLine; ////important add the newlines
            }

            File.WriteAllText(filename, contents);

        }


        private void Init()
        {
            Refresh();
            X = 0;
            Console.CursorLeft = X + margin;
            currLineY = 0;
            Console.CursorTop = currLineY;
        }
        public void Run()
        {
            bool running = true;

            while (running)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;


                if (keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    if (keyInfo.Key == ConsoleKey.X)
                    {
                        //cut
                    }
                    else if (keyInfo.Key == ConsoleKey.C)
                    {
                        //copy
                    }
                    else if (keyInfo.Key == ConsoleKey.V)
                    {
                        //paste
                    }
                }
                else if (keyInfo.Modifiers == 0 || keyInfo.Modifiers == ConsoleModifiers.Shift)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
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
                            break;
                        case ConsoleKey.DownArrow:
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
                            break;
                        case ConsoleKey.RightArrow:  //->
                            if (X < buffer[currLineY].Length)
                            {
                                X++;
                                Console.CursorLeft = X + margin;
                            }
                            break;
                        case ConsoleKey.LeftArrow: //<-
                            if (X > 0)
                            {
                                X--;
                                Console.CursorLeft = X + margin;
                            }
                            break;
                        case ConsoleKey.Home: //
                            X = 0;
                            Console.CursorLeft = X + margin;
                            break;
                        case ConsoleKey.End:
                            X = buffer[currLineY].Length;
                            Console.CursorLeft = X + margin; //put the cursor on the end of the string
                            break;
                        case ConsoleKey.Escape:
                            if (AskWhenExit())
                            {
                                running = false;
                            }
                            else
                            {
                                Refresh();
                                running = true;
                            }
                            break;
                        case ConsoleKey.Delete:
                            if (buffer.Count == 0)
                                break;

                            if (currLineY == buffer.Count)
                                break;

                            if (currLineY == buffer.Count - 1)
                            {

                                if (X < buffer[currLineY].Length)
                                {
                                    buffer[currLineY] = buffer[currLineY].Remove(X, 1);
                                    WriteSentence(buffer[currLineY] + " ");
                                }
                                break;
                            }

                            if (currLineY >= 0)
                            {
                                if (buffer[currLineY] == "") //cursor is in a empty line and user press DEL
                                {
                                    DoBackSpace();
                                    currLineY++;
                                    X = 0;
                                    Console.CursorTop = currLineY;
                                    Console.CursorLeft = X + margin;
                                    break;
                                }

                                if (X == buffer[currLineY].Length) //cursor is in the end of the line and user press DEL
                                {
                                    currLineY++;
                                    X = 0;
                                    DoBackSpace();
                                    break;
                                }

                                if (X < buffer[currLineY].Length)
                                {
                                    buffer[currLineY] = buffer[currLineY].Remove(X, 1);
                                    WriteSentence(buffer[currLineY] + " ");
                                }
                            }

                            break;
                        case ConsoleKey.Backspace:
                            DoBackSpace();
                            break;
                        case ConsoleKey.Enter:
                            DoEnter();
                            break;
                        default:
                            if (buffer.Count == 0)
                                buffer.Add("");

                            if (buffer[currLineY] == null)
                                break;

                            if (X <= buffer[currLineY].Length)
                            {
                                buffer[currLineY] = buffer[currLineY].Insert(X, keyInfo.KeyChar.ToString());
                                X++;
                                WriteSentence(buffer[currLineY]);
                            }

                            break;
                    }//end switch         
                }


                Console.Title = "Current Line: " + currLineY.ToString() + "  X: " + X.ToString();
                Console.CursorVisible = true;
            }//end while
        }

        private void WriteSentence(string sentence)
        {
            Console.CursorTop = currLineY;
            Console.CursorVisible = false;
            Console.CursorLeft = 0 + margin;
            Console.Write(sentence);
            //Console.WriteLine(currLineY.ToString().PadLeft(paddingSize, ' ') + ": " + sentence);
            Console.CursorLeft = X + margin;
            Console.CursorVisible = true;
        }

        private void DoEnter()
        {
            // no puede existir newlines en los buffers
            // en el save se colocara newline a cada buffer item
            // buffer[currLineY] = buffer[currLineY] + Environment.NewLine; //esto se colocara en el save
            // y en el load se eliminaran los newline de cada buffer item

            if (currLineY == buffer.Count)
            {
                buffer.Add("");
            }

            string enterData = buffer[currLineY];
            enterData = enterData.Substring(X, enterData.Length - X);
            buffer[currLineY] = buffer[currLineY].Substring(0, X);
            buffer.Insert(currLineY + 1, enterData);
            Refresh();
            X = 0;
            Console.CursorLeft = X + margin;
            currLineY++;
            Console.CursorTop = currLineY;
        }

        private void DoBackSpace() //Backspace fixed
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
                WriteSentence(buffer[currLineY] + " ");
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
                    Refresh();
                    Console.CursorLeft = X + margin;
                    currLineY--;
                    Console.CursorTop = currLineY;
                }
            }
        }

        private void Refresh()
        {
            int totalLineNumber = buffer.Count;
            paddingSize = totalLineNumber.ToString().Length;
            margin = paddingSize + 2;

            Console.CursorVisible = false;
            Console.Clear();
            int lineNumber = 0;
            foreach (string texto in buffer)
            {
                //Console.WriteLine(texto); //aqui lo imprimimos pero no esta en los buffers
                lineNumber++;
                Console.WriteLine(lineNumber.ToString().PadLeft(paddingSize, ' ') + ": " + texto);
            }
            Console.CursorVisible = true;
        }

        private int getMaxStringLenght()
        {
            int size = 0;
            foreach (string str in buffer)
            {
                if (str.Length > size)
                {
                    size = str.Length;
                }
            }

            return size;
        }

        private bool AskWhenExit()
        {
            bool retorna = false;
            Console.CursorVisible = true;
            Console.CursorLeft = 0;
            Console.CursorTop = buffer.Count + 2;

            int size = getMaxStringLenght() + margin;
            Console.WriteLine("_".PadLeft(size, '_'));

            string msgbox = @"
                +-------------------------------------------------+
                |                                                 |
                |       Do you want to save the changes?          |                
                |                                                 |
                |  +----------+     +----------+    +----------+  |
                |  |   [y]es  |     |   [N]o   |    | [C]ancel |  |
                |  +----------+     +----------+    +----------+  |
                |                                                 |
                +-------------------------------------------------+
                select: ";

            Console.Write(msgbox);

            int tempX = Console.CursorLeft - 1;
            int tempY = Console.CursorTop;

            bool continueAsking = true;

            while (continueAsking)
            {
                int yesNoCancel = Console.Read();
                Console.SetCursorPosition(tempX, tempY);
                Console.Write(" ");
                if (yesNoCancel == 'y' || yesNoCancel == 'Y')
                {
                    
                    FlushKeyboard();
                    Console.WriteLine();
                    Console.Write("                Save as [" + currFilename + "]: ");
                    string filename = Console.ReadLine();

                    if(filename == "")                    
                        SaveFile(currFilename);                                       
                    else
                        SaveFile(filename);                                               
                       
                    
                    continueAsking = false;
                    retorna = true;
                }
                else if (yesNoCancel == 'n' || yesNoCancel == 'N')
                {
                    continueAsking = false;
                    retorna = true;
                }
                else if (yesNoCancel == 'c' || yesNoCancel == 'C')
                {
                    continueAsking = false;
                    retorna = false;
                }
                else
                    continueAsking = true;
            }



            return retorna;

        }

        private void FlushKeyboard()
        {
            while (Console.In.Peek() != -1)
                Console.In.Read();
        }


        

    }//end class
}//end namespace
