using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
    internal class School
    {
        internal void Teach(Person person)
        {
            Console.WriteLine("Educate Person");
        }
    }
}
