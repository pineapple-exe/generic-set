using System;

namespace GenericSet
{
    public class Program
    {
        static void Main(string[] args)
        {

        }

        static void PrintElements(Set<string> set, string title)
        {
            Console.WriteLine($"[{title.ToUpper()}]");

            foreach (string s in set)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("[END OF SET]");
            Console.WriteLine();
        }
    }
}
