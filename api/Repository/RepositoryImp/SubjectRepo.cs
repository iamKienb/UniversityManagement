using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entity;

namespace api.Repository.RepositoryImp
{
    public class SubjectRepo : RepositoryBase<Subject>, ISubjectRepo
    {
        public SubjectRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            
        }
    }
}