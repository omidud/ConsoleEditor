using System;

namespace ConsoleEditor
{
    class Program
    {
        static void Main(string[] args)
        {
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
