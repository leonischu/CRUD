namespace DependencyInjection.Models
{
    public interface IPerson
    {
        IEducationalinstitution School { set; }

        void GetTreatment(IHospital hospital);
        void Study();
        void TakeRefuge();
    }
}