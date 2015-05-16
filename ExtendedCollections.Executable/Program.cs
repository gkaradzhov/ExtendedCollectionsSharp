using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedCollections.Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            var buffer = new CircularBuffer<int>(4);
            buffer.Write(3);
            buffer.Write(4);
            buffer.Write(5);
            buffer.Write(6);

            foreach (var element in buffer)
            {
                Console.WriteLine(element);
            }
        }
    }
}
