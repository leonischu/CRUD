using DapperASPNetCoree.Contracts;
using DapperASPNetCoree.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperASPNetCoree.Controller
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        [Route("get-companies")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepo.GetCompanies();
            return Ok(companies);
        }  

        [HttpGet("{id}",Name = "CompanyById")]
        public async Task<IActionResult>GetCompany(int id)
        {
            var company = await _companyRepo.GetCompany(id);

            if(company is null)
                return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreatinDto company)
        {
            var createdCompany = await _companyRepo.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);

        }

    }
}
