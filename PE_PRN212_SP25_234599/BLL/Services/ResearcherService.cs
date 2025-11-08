using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ResearcherService
    {
        private RepositoryBase<Researcher> _repo = new();

        public List<Researcher> GetAll()
        {
            return _repo.GetAll().ToList();
        }
    }
}
