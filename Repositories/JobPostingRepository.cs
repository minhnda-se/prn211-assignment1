using BusinessObjects.Models;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class JobPostingRepository : IJobPostingRepository
    {
        public bool CreateJobPosting(JobPosting jobPosting)
            => JobPostingDAO.Instance.CreateJobPosting(jobPosting);

        public bool DeleteJobPosting(string id)
            => JobPostingDAO.Instance.DeleteJobPosting(id);

        public JobPosting GetJobPosting(string id)
            => JobPostingDAO.Instance.GetJobPosting(id);

        public List<JobPosting> GetJobPostings()
            => JobPostingDAO.Instance.GetJobPostings();

        public bool UpdateJobPosting(JobPosting jobPosting)
            => JobPostingDAO.Instance.UpdateJobPosting(jobPosting);
    }
}
