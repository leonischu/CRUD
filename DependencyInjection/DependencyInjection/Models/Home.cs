using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
    internal class Home
    {
        internal void Provideshelter(Person person)
        {
            Console.WriteLine("Stay Home");
        }


    }
}
