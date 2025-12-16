
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CollegeApp.Data.Repository
{
    public class StudentRepository :CollegeRepository<Student>,IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dBContext) : base(dBContext) 
        {
            _dbContext = dBContext;

        }

        public Task<List<Student>> GetStudentByFeeStatusAsync(int feeStatus)
        {
            // Write Code to return students having fee status pending
            return null;
        }
    }
}
