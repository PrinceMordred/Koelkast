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

        }
        static bool Steppable(Point pos, int up, int right, char[,] arr)
        {
            char temp = arr[pos.X + right, pos.Y - up];
            return temp.Equals('.') || temp.Equals('?');

        }
        static void Step(Point pos, int up, int right, char[,] arr)
        {
            
        }
        static char[,] Swap(Point a, Point b, char[,] arr)
        {
            char temp = arr[a.X, a.Y];
            arr[a.X, a.Y] = arr[b.X, b.Y];
            arr[b.X, b.Y] = temp;
            return arr;
        }
    }
}