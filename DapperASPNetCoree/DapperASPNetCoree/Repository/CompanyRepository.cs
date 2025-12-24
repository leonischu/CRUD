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

        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM Companies WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
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

        public async Task<Company> GetCompanyByEmployeeId(int id)
        {
            var procedureName = "ShowCompanyByEmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using(var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Company>(procedureName, parameters,commandType:CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task<Company> GetMultipleResults(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id;" + "SELECT * FROM Employees WHERE CompanyId = @Id";
            // One gets the company and another gets the employee of that company 



            using(var connection = _context.CreateConnection()) 
                
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
                // Execute both select queries at once, allows reading from multiple result sets



            {
                 var company = await multi.ReadSingleOrDefaultAsync<Company>();
                if(company is not null)
                    company.Employees = (await multi.ReadAsync<Employee>()).ToList();

                /* Only read employees if company exists

                    Reads second SELECT

                        Maps rows → List<Employee>
    
                    Assigns it to company.Employees  */

                return company;
            }


           
        }

        public async Task<List<Company>> MultipleMapping()
        {
            var query = "SELECT * FROM Companies c JOIN Employees e ON c.Id = e.CompanyId";
            using(var connection = _context.CreateConnection())
            {
                var companyDict = new Dictionary<int, Company>();
                var companies = await connection.QueryAsync<Company, Employee, Company>(query, (company, employee) =>
                {
                    if (!companyDict.TryGetValue(company.Id, out var currentCompany))
                    {
                        currentCompany = company;
                        companyDict.Add(company.Id, currentCompany);

                    }
                    currentCompany.Employees.Add(employee);
                    return currentCompany;
                });
                return companies.Distinct().ToList();
            }
            
        }

        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = "Update Companies SET Name = @Name, Address =@Address, Country = @Country WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);   
            }
        }


        public async Task CreateMultipleCompanies(List<CompanyForCreatinDto> companies)
        {
            var query = "INSERT INTO Companies(Name,Address,Country) VALUES (@Name,@Address,@Country)";

            using(var connection = _context.CreateConnection()) {
                connection.Open();
                using(var transaction = connection.BeginTransaction()) {    
                    foreach(var company in companies)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("Name",company.Name,DbType.String);
                        parameters.Add("Address",company.Address,DbType.String);
                        parameters.Add("Country",company.Name,DbType.String);
                        await connection.ExecuteAsync(query, parameters,transaction : transaction);
                    }
                    transaction.Commit();
        }
    }
}
