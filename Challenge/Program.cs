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

            int lineCounter = 0;
            foreach (var line in reader.Read(FILE_PATH))
            {
                user.Load(parser.Parse(line));
                ++lineCounter;
                if (lineCounter % 1000 == 0)
                {
                    Console.WriteLine($"Lines parsed {lineCounter}");
                }
            }
            Console.WriteLine("End of reading from file.");
            Console.WriteLine($"Users in file {user.Count()}");

        Console.ReadLine();
        }
    }
}
