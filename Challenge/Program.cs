using System;
using System.Collections.Generic;
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
            foreach (var line in reader.Read(FILE_PATH))
            {
                user.Load(parser.Parse(line));
                ++lineCounter;
                if (lineCounter % 100000 == 0)
                {
                    Console.WriteLine($"Lines parsed {lineCounter}");
                }
            }
            Console.WriteLine("End of reading from file.");
            Console.WriteLine($"Users in file {user.Count()}");
            Console.WriteLine("Calculate Distance between nodes.");
            while (true)
            {
                Console.Write("Please type root node: ");
                string startNode = Console.ReadLine();
                Console.Write("Please type final node: ");
                string destinationNode = Console.ReadLine();
                Console.Write("Calculating distance : ");
                var distance = algorithm.FindDistance(user, startNode, destinationNode);
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
