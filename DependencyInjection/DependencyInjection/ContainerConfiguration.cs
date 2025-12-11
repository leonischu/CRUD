using Autofac;
using DependencyInjection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class ContainerConfiguration
    {
        public static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType <Person>().As<IPerson>();
            builder.RegisterType<Home>().As<IHome>();
            builder.RegisterType<College>().As<IEducationalinstitution>();
            builder.RegisterType<Hospital>().As<IHospital>();
            return builder.Build();
        }


    }
}
