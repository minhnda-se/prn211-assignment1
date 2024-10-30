using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class JobPostingDAO
    {
        private CandidateManagementContext context;
        private static JobPostingDAO instance = null;
        public static JobPostingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JobPostingDAO();
                }
                return instance;
            }
        }
        public JobPostingDAO()
        {
            context = new CandidateManagementContext();
        }

        public List<JobPosting> GetJobPostings()
        {
            return context.JobPostings.ToList();
        }

        public JobPosting GetJobPosting(string id)
        {
            return context.JobPostings.SingleOrDefault(jp => jp.PostingId.Equals(id));
        }

        public bool CreateJobPosting(JobPosting jobPosting)
        {
            var jp = GetJobPosting(jobPosting.PostingId);
            if (jp == null)
            {
                context.JobPostings.Add(jobPosting);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateJobPosting(JobPosting jobPosting)
        {
            var jp = GetJobPosting(jobPosting.PostingId);
            if (jp != null)
            {
                jp.JobPostingTitle = jobPosting.JobPostingTitle;
                jp.Description = jobPosting?.Description;
                jp.PostedDate = jobPosting.PostedDate;
                jp.PostingId = jobPosting.PostingId;
                context.JobPostings.Update(jp);
                return true;
            }
            return false;
        }

        public bool DeleteJobPosting(string id)
        {
            var jp = GetJobPosting(id);
            if (jp != null)
            {
                context.JobPostings.Remove(jp);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
