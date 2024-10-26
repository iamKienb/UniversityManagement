using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.RepositoryImp
{
    public class StudentRepo: RepositoryBase<Student>, IStudentRepo
    {
        public StudentRepo(ApplicationDBContext applicationDBContext):base(applicationDBContext)
        {
            
        }

        public async Task<Student> FindUserByEmailAsync(string email)
        {
            return await _context.Set<Student>().FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}