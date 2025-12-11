using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
    public class Person : IPerson
    {
        private IHome _home;
        private IEducationalinstitution _school;





        public IEducationalinstitution School
        {
            set
            {
                _school = value;

            }
        }
        public Person(IHome home)
        {
            _home = home;

        }

        public void TakeRefuge()
        {
            _home.Provideshelter(this);
        }
        public void Study()
        {
            if (_school != null)
            {
                _school.Teach(this);
            }
        }

        public void GetTreatment(IHospital hospital)
        {
            hospital.Cure(this);
        }
    }
}
