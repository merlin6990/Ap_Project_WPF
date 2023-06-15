
using System;
namespace my
{
    public abstract class Abs
    {
        int i;
        int j;
        public Abs(int i, int j)
        {
            this.i = i;
            this.j = j;
            Console.WriteLine(i + j);
        }
    }
    public class program
    {
        static void Main()
        {
            Abs a = new Abs(2,3);
        }
    }
}




