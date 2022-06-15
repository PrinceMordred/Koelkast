using System;
using System.Drawing;

namespace Koelkast
{
    internal class Program
    {
        static void Main()
        {
            //innits
            string[] inp = Console.ReadLine().Split();
            int b = int.Parse(inp[0]);
            int h = int.Parse(inp[1]);
            string m = inp[2];
            string outp = "";
            //make map
            char[,] map = new char[b,h];
            Point koelkastPos;
            Point goal;
            for (int i = 0; i < h; i++)
            {
                inp = Console.ReadLine().Split();
                for (int j = 0; j < b; j++)
                {
                    map[i, j] = inp[0][j];
                    if (map[i, j].Equals('!'))
                        koelkastPos = new Point(i, j);
                    else if (map[i,j].Equals('?'))
                        goal = new Point(i, j);
                }
            }
            //find solution
            BreadthFirstSearch(map);
            //print solution
            Console.WriteLine(m.Equals("P") ? outp : outp.Length);
        }

        private static void BreadthFirstSearch(char[,] arr)
        {
            // make arrays for tests
            char[,] state = new char[arr.GetLength(0), arr.GetLength(1)];
            arr.CopyTo(state, 0);
            bool[,] visited = new bool[arr.GetLength(0), arr.GetLength(1)];
            //start breadth search based on if step has already been visited or if step is possible
            for (int i = 0; i < 4; i++)
            {
                if (Steppable(())

            }
        }

        static bool Steppable(Point pos, int up, int right, char[,] arr)
        {
            char temp = arr[pos.X + right, pos.Y - up];
            return temp.Equals('.') || temp.Equals('?');

        }
        static void Step(Point a, Point b, char[,] arr)
        {
            char temp = arr[a.X, a.Y];
            arr[a.X, a.Y] = arr[b.X, b.Y];
            arr[b.X, b.Y] = temp;
        }
    }
}