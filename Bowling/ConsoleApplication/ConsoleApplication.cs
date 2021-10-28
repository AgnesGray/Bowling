using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication
{
    public class ConsoleApplicationClass
    {
        public string readConsole()
        {
            return Console.ReadLine();
        }
        //exemplu consumer
        public void writeInConsole(string a)
        {
            Console.WriteLine(a);
        }
    }
}
