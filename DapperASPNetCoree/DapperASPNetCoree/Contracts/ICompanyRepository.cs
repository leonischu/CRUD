using DapperASPNetCoree.Dto;
using DapperASPNetCoree.Entities;

namespace DapperASPNetCoree.Contracts
{
    public interface ICompanyRepository
    {

        //Any class that implements this interface must have GetCompanies and GetCompany methods.


        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForCreatinDto company);
    }


}
