using BusinessObjects.Models;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CandidateProfileRepository : ICandidateProfileRepository
    {
        public bool CreateCandidateProfile(CandidateProfile candidateProfile)
            => CandidateProfileDAO.Instance.CreateCandidateProfile(candidateProfile);

        public bool DeleteCandidateProfile(string id)
            => CandidateProfileDAO.Instance.DeleteCandidateProfile(id);

        public CandidateProfile GetCandidate(string id)
            => CandidateProfileDAO.Instance.GetCandidate(id);

        public CandidateProfile GetCandidateByName(string name)
            => CandidateProfileDAO.Instance.GetCandidateByName(name);

        public List<CandidateProfile> GetCandidates()
            => CandidateProfileDAO.Instance.GetCandidates();

        public bool UpdateCandidateProfile(CandidateProfile candidateProfile)
            => CandidateProfileDAO.Instance.UpdateCandidateProfile(candidateProfile);

    }
}
