using System;

namespace ConsoleEditor
{
    class Program
    {

        //static string Center(string str,int width)
        //{
        //    if (width <= str.Length)
        //        return str;

        //    int length = str.Length;
        //    int padSize = (width / 2) - (length / 2);
        //    string outStr = "".PadLeft(padSize, ' ') + str + "".PadRight(padSize, ' ');

        //    return outStr;
        //}

        static void Main(string[] args)
        {

            //string str = "Hello World!";
            //int width = 80;
            //int length = str.Length;
            //int size = (width / 2) - (length / 2);

            //string centerText = "".PadLeft(size, ' ') + str + "".PadRight(size, ' ');

           // Console.WriteLine(Center(str,17));
           // Console.ReadLine();





            Editor editor;

            if (args.Length > 0)
            {
                string filename = args[0];
                editor = new Editor(filename);               
            }
            else
            {
                editor = new Editor();                
            }

            editor.Run();
        }
    }
}
