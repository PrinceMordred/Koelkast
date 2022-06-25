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
            Queue<char[,]> qSet = new Queue<char[,]>();
            Point koelkastPos = new Point();
            Point goal = new Point();
            Point koen = new Point();
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
            BreadthFirstSearch(map, koen, koelkastPos, goal, hSet, qSet, true);


            //print solution
            Console.WriteLine(m.Equals("P") ? outp : outp.Length);
        }

        private static void BreadthFirstSearch(char[,] arr, Point koen, Point koelkast, Point goal, HashSet<int> hMap, Queue<char[,]> queue, bool first)
        {
            
            //check if node is searched
            
            while (queue.Count>0 || first)
            {
                first = false;
                
                //if()

                for (int i = 0; i < 4; i++)
                {
                    int x = 0;
                    int y = 0;
                    if (i == 0)
                        y = 1;
                    if (i == 1)
                        x = 1;
                    if (i == 2)
                        y = -1;
                    if (i == 3)
                        x = -1;
                    Point dir = new Point(x,y);

                    
                    if (Steppable(koen, new Point(koen.X + dir.X, koen.Y + dir.Y), koelkast, arr))
                    {
                        char[,] succ = new char[arr.GetLength(0), arr.GetLength(1)];
                        for (int a = 0; a < arr.GetLength(1); a++)
                        {
                            for (int b = 0; b < arr.GetLength(0); b++)
                            {
                                succ[b, a] = arr[b, a];
                            }
                        }

                        Step(koen, new Point(koen.X +dir.X, koen.Y + dir.Y),koelkast, succ);
                        queue.Enqueue(succ);
                    }
                }
            }
            
        }

        static bool Steppable(Point a, Point b, Point koelkast, char[,] arr)
        {
            // if both points are '.', '+' or '!'
            char aa = arr[a.X, a.Y]; 
            char bb = arr[b.X, b.Y];
            if (b.Equals(koelkast))
            {
                int xDiff = a.X - b.X;
                int yDiff = a.Y - b.Y;
                Point nextKPos = new Point(b.X-xDiff, b.Y -yDiff);
                return Steppable(b, nextKPos, koelkast, arr);
            }
            return (aa.Equals('.') || aa.Equals('!') || aa.Equals('+')) && (bb.Equals('.') || bb.Equals('!') || bb.Equals('+'));

        }
        static void Step(Point a, Point b, Point koelkast, char[,] arr)
        {
            char temp = arr[a.X, a.Y];
            arr[a.X, a.Y] = arr[b.X, b.Y];
            arr[b.X, b.Y] = temp;
            if (b.Equals(koelkast))
            {
                int xDiff = a.X - b.X;
                int yDiff = a.Y - b.Y;
                Point nextKPos = new Point(b.X - xDiff, b.Y - yDiff);
                Step(b, nextKPos, koelkast, arr);
            }
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