using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICandidateProfileRepository 
    {
        List<CandidateProfile> GetCandidates();
        CandidateProfile GetCandidate(string id);
        CandidateProfile GetCandidateByName(string name);
        bool CreateCandidateProfile(CandidateProfile candidateProfile);
        bool UpdateCandidateProfile(CandidateProfile candidateProfile);
        bool DeleteCandidateProfile(string id);
    }
}
