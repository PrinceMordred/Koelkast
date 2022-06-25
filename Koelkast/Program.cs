using System;
using System.Collections.Generic;

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
            int koelkastPosX = 0;
            int koelkastPosY = 0;
            int koenX = 0;
            int koenY = 0;

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
                            koelkastPosX = j ;
                            koelkastPosY = i;
                            map[j, i] = 2;
                            break;
                        case '?':
                            map[j, i] = 3;
                            break;
                        case '+':
                            koenX = j;
                            koenY = i;
                            map[j, i] = 4;
                            break;
                        default:
                            map[j, i] = 0;
                            break;
                    }
                }
            }
            
            //print solution

            uint state = BreadthFirstSearch(map, koenX, koenY, koelkastPosX, koelkastPosY, qSet);
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

        private static uint BreadthFirstSearch(byte[,] arr, int koenX, int koenY, int koelkastX, int koelKastY, Queue<uint> queue)
        {

            //check if node is searched
            uint state = State(koenX, koenY, koelkastX, koelKastY);
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
                    (int kkpX, int kkpY) = KoelkastPos(v);
                    if (arr[kkpX, kkpY] == 3)
                    {
                        hSet.Add(v, u);
                        return v;
                    }

                    if (!hSet.ContainsKey(v))
                    {
                        hSet.Add(v, u);
                        queue.Enqueue(v);
                    }
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

            (int kkpX,int  kkpY) = KoelkastPos(state);
            (int kpX, int kpY) = KoenPos(state);

            kpX += right;
            kpY += down;

            if (arr[kpX, kpY] == 0)
                return 0;
            if (kkpX == kpX && kkpY == kpY)
            {
                kkpX += right;
                kkpY += down;
                if (arr[kkpX, kkpY] == 0)
                    return 0;  
            }
            return State(kpX, kpY, kkpX, kkpY);
                
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
        static uint State(int koenX, int koenY, int koelkastX, int koelkastY)
        {
            // (koen)(koelkast)
            return (uint)(koenX | (koenY << 8) | (koelkastX << 16) | (koelkastY << 24)) ;
        }
        static (int, int) KoelkastPos(uint state) => ((int)(state >> 16) & 255, (int)(state >> 24) & 255);
        static (int, int) KoenPos(uint state) => ((int)state & 255, (int)(state >> 8) & 255);
        static string Directions(uint endState)
        {
            string res = "";
            uint old = endState;
            uint current = hSet[endState];
            while (current != 0)
            {
                (int kCX, int kCY) = KoenPos(current);
                (int kOX, int kOY)= KoenPos(old);
                int offsetX = (kOX - kCX);
                int offsetY = (kOY- kCY);
                old = current;
                current = hSet[current];

                res = "NWXES"[offsetX + offsetY * 2 + 2] + res; //credits Olaf, we hadden samen bedacht dat strings arrays zijn <3 (dit is geen plagiaat)
            }
            return res;
        }
    }
}