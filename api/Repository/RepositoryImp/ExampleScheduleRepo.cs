using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entity;

namespace api.Repository.RepositoryImp
{
    public class ExampleScheduleRepo: RepositoryBase<ExamSchedule>, IExampleScheduleRepo
    {
        public ExampleScheduleRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            
        }
    }
}