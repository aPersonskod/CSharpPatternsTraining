// See https://aka.ms/new-console-template for more information
using System;

namespace ConsoleApp2
{
    class Program
    {
        /*static double F(int a, int b){return a +b;}
        static double F(double a, double b){return a +b;}
        static double F(short a, double b){return a +b;}
        static double F(short a, int b){return a +b;}

        public static void Main()
        {
            F(2, 3.5);
        }*/
        public static void Main()
        {
            var acts = new List<Action>();
            for (int i = 0; i < 3; i++)
            {
                acts.Add(() =>
                {
                    Console.WriteLine(i);
                });
            }

            acts[1]();
        }
    }
}
