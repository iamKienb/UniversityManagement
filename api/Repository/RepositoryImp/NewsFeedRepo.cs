using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.RepositoryImp
{
    public class NewsFeedRepo: RepositoryBase<NewsFeed>, INewsFeedRepo
    {
        public NewsFeedRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            
        }

        public async Task<List<NewsFeed>> GetNewsFeedByStudentIdAsync(int studentId)
        {
            var feedList = await _context.Set<NewsFeed>()
            .Include(f => f.Student)
            .Where(f => f.StudentId == studentId)
            .ToListAsync();
            return feedList;
        }

    }
}