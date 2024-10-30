using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class CandidateProfileDAO
    {
        private readonly CandidateManagementContext context;
        private static CandidateProfileDAO instance = null;
        public static CandidateProfileDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CandidateProfileDAO();
                }
                return instance;
            }
        }
        public CandidateProfileDAO()
        {
            context = new CandidateManagementContext();
        }

        public List<CandidateProfile> GetCandidates()
        {
            return context.CandidateProfiles.Include(cp => cp.Posting).ToList();
        }

        public CandidateProfile GetCandidateByName(string name)
        {
            return context.CandidateProfiles.SingleOrDefault(cp => cp.Fullname.Equals(name));
        }

        public CandidateProfile GetCandidate(string id)
        {
            return context.CandidateProfiles.SingleOrDefault(cp => cp.CandidateId.Equals(id));
        }

        public bool CreateCandidateProfile(CandidateProfile profile)
        {
            var candidateProfile = GetCandidate(profile.CandidateId);
            if (candidateProfile == null)
            {
                context.CandidateProfiles.Add(profile);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateCandidateProfile(CandidateProfile profile)
        {
            var updateProfile = GetCandidate(profile.CandidateId);
            if (updateProfile != null)
            {
                updateProfile.Fullname = profile.Fullname;
                updateProfile.ProfileUrl = profile.ProfileUrl;
                updateProfile.Birthday = profile.Birthday;
                updateProfile.PostingId = profile.PostingId;
                updateProfile.ProfileShortDescription = profile.ProfileShortDescription;
                context.CandidateProfiles.Update(updateProfile);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteCandidateProfile(string id)
        {
            var candidateProfile = GetCandidate(id);
            if (candidateProfile == null)
            {
                return false;
            }
            context.CandidateProfiles.Remove(candidateProfile);
            context.SaveChanges();
            return true;
        }

    }
}
