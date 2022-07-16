using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROKbot
{
    public static class Path
    {
        //right     1
        //left      0
        //bottom    3
        //top       2

        public static int cycle { get; set; }
        public static int iteration { get; set; }
        public static int way { get; set; }
        public static readonly int[][] paths = new int[][]{new int[]{ 3,3,3,3,3,0,2,2,2,2,2,2,0 },
                                                           new int[]{ 2,2,2,2,2,2,2,2,2,2,0,3,3,3,3,3,3,3,3,3,3,0},
                                                           new int[]{ 1,1,1,1,1,2,0,0,0,0,0,2 } };

        static Path()
        {
            string? d = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (File.Exists(d+"\\path.txt"))
            {
                var list = new List<List<int>>();
                foreach (string line in File.ReadLines(d+"\\path.txt"))
                {
                    if (!line.Contains('/') && !String.IsNullOrEmpty(line))
                    {
                        list.Add(new List<int>());
                        foreach (var c in line)
                        {
                            list[list.Count - 1].Add(Int32.Parse(c.ToString()));
                        }
                    }
                }
                paths = list.Select(a => a.ToArray()).ToArray();
            }
            else
            {
                paths = new int[1][] { SpyralArrayFormatted().ToArray() };
            }
        }

        public static int GetDirection()
        {
            var path = paths[way][iteration];
            Maintenance();
            return path;
        }

        public static int GetSpiralPath()
        {
            var path = paths[way][iteration];
            Maintenance();
            return path;
        }


        public static void ZeroPath()
        {
            iteration = 0;
            way = 0;
            cycle = 0;
        }

        public static (int,int,int) GetData()
        {
            return (way, cycle, iteration);
        }

        public static bool IsEnd()
        {
            if (cycle > 3)
            {
                NextPath();
                return true;
            }
            else
                return false;
        }

        public static void NextPath()
        {
            way++;
            if (way >= paths.Count())
                way = 0;
            iteration = 0;
            cycle = 0;
        }

        public static void Maintenance()
        {
            iteration++;
            if (iteration >= paths[way].Count())
            {
                iteration = 0;
                cycle++;
            }
            if (cycle > 4)
            {
                cycle = 0;
                way++;
            }
            if (way >= paths.Count())
            {
                way = 0;
            }
        }

        public static IEnumerable<int> SpyralArrayFormatted()
        {
            foreach (var t in SpyralArray())
            {
                switch (t)
                {
                    case 0: { yield return 3; break; }
                    case 1: { yield return 1; break; }
                    case 2: { yield return 2; break; }
                    case 3: { yield return 0; break; }
                }
            }
        }

        public static IEnumerable<int> SpyralArray()
        {
            var d = 0;
            for (int i = 0; i < 100; i++)
            {
                var k = i % 4;
                if (i % 2 == 0) 
                    d++;
                for (int j = 0; j < d; j++)
                    yield return k;
            }
        } 
    }
}
