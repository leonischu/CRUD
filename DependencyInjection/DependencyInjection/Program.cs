using Autofac;
using DependencyInjection;
using DependencyInjection.Models;
namespace DemoApp
{
internal class Program
    {
        static void Main(string[] args)
        {
            //Home home = new Home();
            //Person person = new Person(home);
            //person.TakeRefuge();

            //person.School = new College();
            //person.Study();
            //person.GetTreatment(new Hospital());

            var container = ContainerConfiguration.Configure();
            using(var scope = container.BeginLifetimeScope())
            {
                IPerson person = scope.Resolve<IPerson>();
                person.TakeRefuge();
                person.GetTreatment(scope.Resolve <IHospital>());

                person.School = scope.Resolve<IEducationalinstitution>();
                person.Study();
            }


        }
    }

}

