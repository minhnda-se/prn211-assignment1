using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IJobPostingRepository
    {
        List<JobPosting> GetJobPostings();
        JobPosting GetJobPosting(string id);
        bool CreateJobPosting(JobPosting jobPosting);
        bool UpdateJobPosting(JobPosting jobPosting);
        bool DeleteJobPosting(string id);

    }
}
