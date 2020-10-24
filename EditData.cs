using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEditor
{
    public class EditData
    {
        public Point bufferPosition { get; set; }
        public List<string> buffer { get; set; }
        public string currFilename { get; set; }
        public int padding { get; set; }
        public int margin { get; set; }

        public EditData()
        {
            bufferPosition = new Point();
            buffer = new List<string>();
            currFilename = "";
            padding = 0;
            margin = 0;
        }
    }//end class
}//end namespace
