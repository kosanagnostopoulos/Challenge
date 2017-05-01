using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    class Program
    {
        private const string FILE_PATH = @"..\..\SocialNetwork.txt";

        static void Main(string[] args)
        {
            ILineReader reader = new LineReader();
            ILineParser parser = new LineParser();
            IUserCollection user = new UserCollection();
            IShortestPath algorithm = new ShortestPath(new UserCollection(), new UserCollection());

            Console.CancelKeyPress += delegate {
                Environment.Exit(0);
            };
            Console.WriteLine("Type any time CTRL-C to exit application.");
            Console.WriteLine("Parsing file.");
            int lineCounter = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var line in reader.Read(FILE_PATH))
            {
                user.Load(parser.Parse(line));
                ++lineCounter;
                if (lineCounter % 100000 == 0)
                {
                    Console.WriteLine($"Lines parsed {lineCounter}");
                }
            }
            watch.Stop();
            Console.WriteLine("Readfile: " + watch.ElapsedMilliseconds);
            Console.WriteLine("End of reading from file.");
            Console.WriteLine($"Users in file {user.Count()}");
            Console.WriteLine("Calculate Distance between nodes.");
            while (true)
            {
                //Console.Write("Please type root node: ");
                //string startNode = Console.ReadLine();

                string startNode = "MYLES_JEFFCOAT";
                //Console.Write("Please type final node: ");
                //string destinationNode = Console.ReadLine();
                string destinationNode = "NEWTON_OSEN";
                Console.Write("Calculating distance : ");
                watch.Restart();
                var distance = algorithm.FindDistance(user, startNode, destinationNode);
                watch.Stop();
                Console.WriteLine("Time elapsed " + watch.ElapsedMilliseconds);
                if (distance == -1)
                {
                    Console.WriteLine("Cannot find connection between nodes.");
                }
                else
                {
                    Console.WriteLine(distance);
                }
            }
        }
    }
}
