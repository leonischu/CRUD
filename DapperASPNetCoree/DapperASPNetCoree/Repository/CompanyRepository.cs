using Dapper;
using DapperASPNetCoree.Context;
using DapperASPNetCoree.Contracts;
using DapperASPNetCoree.Dto;
using DapperASPNetCoree.Entities;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Text;

namespace DapperASPNetCoree.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context) => _context = context;

        public async Task<Company> CreateCompany(CompanyForCreatinDto company)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)" + "SELECT CAST(SCOPE_IDENTITY() AS int)"; 
            var parameters = new DynamicParameters();
            //var strsql = new StringBuilder();
            //strsql.AppendFormat(@"")
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query,parameters);

                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };

                return createdCompany;


            }

        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";
            using (var connection = _context.CreateConnection())
            {
            
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
             var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
                return company;


            }
        }
    }
}
