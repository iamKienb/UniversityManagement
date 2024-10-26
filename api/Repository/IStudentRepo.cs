using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entity;

namespace api.Repository
{
    public interface IStudentRepo: IRepositoryBase<Student>
    {
        Task<Student> FindUserByEmailAsync(string email);
       
    }
}