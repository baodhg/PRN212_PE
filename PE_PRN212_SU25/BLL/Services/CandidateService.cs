using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CandidateService
    {
        private RepositoryBase<Candidate> _candidateRepository = new();

        public List<Candidate> GetAll()
        {
            return _candidateRepository.GetAll().ToList();
        }
    }
}
