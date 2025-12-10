using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Models
{
    public class Person
    {
        private Home _home;
        private School _school;

        private Hospital _hospital;

        public Person()
        {
            _home = new Home();
            _school = new School();
            _hospital = new Hospital();
        }

        public void TakeRefuge()
        {
            _home.Provideshelter(this);
        }
        public void Study()
        {
            _school.Teach(this);
        }

        public void GetTreatment() { 
        _hospital.Cure(this);
        }
    }
}
