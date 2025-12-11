using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
    public class School : IEducationalinstitution
    {
        public void Teach(IPerson person)
        {
            Console.WriteLine("Educate Person in school");
        }
    }
}
