using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.RepositoryImp
{
    public class MajorRepo : RepositoryBase<Major>, IMajorRepo
    {
        public MajorRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {

        }

        public async Task<Major> FindByCode(string SubjectCode)
        {
            return await _context.Set<Major>().FirstOrDefaultAsync(m => m.SubjectCode == SubjectCode);
        }
    }
}