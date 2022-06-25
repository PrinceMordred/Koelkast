using System;
using System.Drawing;

namespace Koelkast
{
    internal class Program
    {
        static Dictionary<uint, uint> hSet = new Dictionary<uint, uint>();
        static void Main()
        {
            //innits
            string[] inp = Console.ReadLine().Split();
            int b = int.Parse(inp[0]);
            int h = int.Parse(inp[1]);
            string m = inp[2];
            string outp = "";

            //make map
            byte[,] map = new byte[b, h];

            Queue<uint> qSet = new Queue<uint>();
            Point koelkastPos = new Point();
            Point koen = new Point();
            //find positions
            for (int i = 0; i < h; i++)
            {
                string inp2 = Console.ReadLine();
                for (int j = 0; j < b; j++)
                {
                    switch (inp2[j])
                    {
                        case '.':
                            map[j, i] = 1;
                            break;
                        case '!':
                            koelkastPos = new Point(j, i);
                            map[j, i] = 2;
                            break;
                        case '?':
                            map[j, i] = 3;
                            break;
                        case '+':
                            koen = new Point(j, i);
                            map[j, i] = 4;
                            break;
                        default:
                            map[j, i] = 0;
                            break;
                    }
                }
            }

            //print solution

            uint state = BreadthFirstSearch(map, koen, koelkastPos, qSet);
            if (state == 0)
                Console.WriteLine("No solution");
            else
            {
                outp = Directions(state);
                Console.WriteLine(outp.Length);
                if (m.Equals("P"))
                    Console.WriteLine(outp);
            }
        }

        private static uint BreadthFirstSearch(byte[,] arr, Point koen, Point koelkast, Queue<uint> queue)
        {

            //check if node is searched
            uint state = State(koen, koelkast);
            hSet.Add(state, 0);
            queue.Enqueue(state);
            while (queue.Count > 0)
            {
                uint u = queue.Dequeue();
                uint[] next = Successor(u, arr);
                foreach (uint v in next)
                {
                    if (v == 0)
                        continue;
                    Point kkp = KoelkastPos(v);
                    if (arr[kkp.X, kkp.Y] == 3)
                    {
                        hSet.Add(v, u);
                        return v;
                    }

                    try
                    {
                        hSet.Add(v, u);
                        queue.Enqueue(v);
                    }
                    catch { };
                }
            }
            return 0;
        }

        private static uint[] Successor(uint u, byte[,] arr)
        {
            uint[] res = new uint[4];
            res[0] = Step(u, 1, 0, arr);
            res[1] = Step(u, 0, 1, arr);
            res[2] = Step(u, -1, 0, arr);
            res[3] = Step(u, 0, -1, arr);
            return res;
        }

        static uint Step(uint state, int right, int down, byte[,] arr)
        {
            Point kkp = KoelkastPos(state);
            Point kp = KoenPos(state);

            kp.X += right;
            kp.Y += down;

            if (arr[kp.X, kp.Y] == 0)
                return 0;
            if (kkp.X == kp.X && kkp.Y == kp.Y)
            {
                kkp.X += right;
                kkp.Y += down;
                if (arr[kkp.X, kkp.Y] == 0)
                    return 0;  
            }
            return State(kp, kkp);
                
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
        static uint State(Point koen, Point koelkast)
        {
            // (koen)(koelkast)
            return (uint)(koen.X | (koen.Y << 8) | (koelkast.X << 16) | (koelkast.Y << 24)) ;
        }
        static Point KoelkastPos(uint state) => new Point((int)(state >> 16) & 255, (int)(state >> 24) & 255);
        static Point KoenPos(uint state) => new Point((int)state & 255, (int)(state >> 8) & 255);
        static string Directions(uint endState)
        {
            string res = "";
            uint old = endState;
            uint current = hSet[endState];
            while (current != 0)
            {
                Point kC = KoenPos(current);
                Point kO = KoenPos(old);
                Point offset = new Point(kO.X - kC.X,  kO.Y- kC.Y);
                old = current;
                current = hSet[current];

                res = "NWXES"[offset.X + offset.Y * 2 + 2] + res; //credits Olaf, we hadden samen bedacht dat strings arrays zijn <3 (dit is geen plagiaat)
            }
            return res;
        }
    }
}