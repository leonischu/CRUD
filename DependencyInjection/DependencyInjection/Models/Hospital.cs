using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
    public class Hospital : IHospital
    {
        public void Cure(IPerson person)
        {
            Console.WriteLine("Cure Person");
        }
    }
}
