using System;

namespace ConsoleEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Editor editor = new Editor("demo.txt");

            editor.Run();
        }
    }
}
