
namespace ConsoleEditor
{
    //for no depent of System.Drawing 
    //we just need a Point    
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
