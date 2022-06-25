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
            HashSet<int> hSet = new HashSet<int>();
            Point koelkastPos;
            Point goal;
            Point koen;
            //find positions
            for (int i = 0; i < h; i++)
            {
                inp = Console.ReadLine().Split();
                for (int j = 0; j < b; j++)
                {
                    map[j, i] = inp[0][j];
                    if (map[j, i].Equals('!'))
                        koelkastPos = new Point(j, i);
                    else if (map[j,i].Equals('?'))
                        goal = new Point(j, i);
                    else if (map[j, i].Equals('+'))
                        koen = new Point(j, i);
                }
            }
            //find solution
            BreadthFirstSearch(map, hSet);

            //print map
            //Console.WriteLine(printMap(map));

            //print solution
            Console.WriteLine(m.Equals("P") ? outp : outp.Length);
        }

        private static void BreadthFirstSearch(char[,] arr, HashSet<int> hMap)
        {
            // make arrays for tests
            char[,] state = new char[arr.GetLength(0), arr.GetLength(1)];
            char[] stateList = new char[arr.Length];
            int b = arr.GetLength(0);
            int h = arr.GetLength(1);
            if(true) //pos !in hMap) ____________________________________________________
            {

                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < b; j++)
                    {
                        state[j, i] = arr[j, i];
                    }
                }
                hMap.Add(state); // add to hMap

                //start breadth search based on if step has already been visited or if step is possible and check for end
                //then queue next nodes

                for (int i = 0; i < 4; i++) // 4 because of directions
                {
                    if (Steppable(()) &&
    
            }
            }
            
        }

        static bool Steppable(Point pos, int up, int right, char[,] arr)
        {
            char temp = arr[pos.X + right, pos.Y - up];
            return temp.Equals('.') || temp.Equals('!');

        }
        static void Step(Point a, Point b, char[,] arr)
        {
            char temp = arr[a.X, a.Y];
            arr[a.X, a.Y] = arr[b.X, b.Y];
            arr[b.X, b.Y] = temp;
            // add fridge move
        }
        static string printMap(char[,] m)
        {
            string outp = "";
            for (int i = 0; i < m.GetLength(1); i++)
            {
                for (int j = 0; j < m.GetLength(0); j++)
                {
                    outp += m[j, i];
                }
                outp += "\n";
            }
            return outp;
        }
    }
}