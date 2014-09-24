using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.TestFramework.Test
{
    class RandomTest
    {
        public static void DoTest()
        {
            Random random = new Random();

            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("Random.Next({0})={1}", i, random.Next(3));
            }

            Console.ReadKey();
        }
    }
}
