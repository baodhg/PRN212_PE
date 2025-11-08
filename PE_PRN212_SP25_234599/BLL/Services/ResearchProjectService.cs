using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ResearchProjectService
    {
        private RepositoryBase<ResearchProject> _repo = new();

        public List<ResearchProject> GetAllProjects()
        {
            return _repo.GetAll().Include(rp => rp.LeadResearcher).ToList();
        }

        public List<ResearchProject> SearchResearchProjects(string keyword)
        {
            var result = _repo.GetAll().Include(rp => rp.LeadResearcher).AsNoTracking();
            string normalizedKeyword = keyword.Trim().ToLowerInvariant();

            if (keyword.IsNullOrEmpty())
            {
                return result.ToList();
            }

            if (!keyword.IsNullOrEmpty())
            {
                result = result.Where(rp =>
                    rp.ProjectTitle.ToLower().Contains(normalizedKeyword) ||
                    rp.ResearchField.ToLower().Contains(normalizedKeyword));
            }
            return result.ToList();
        }

        public void DeleteProject(ResearchProject project)
        {
            _repo.Delete(project);
        }
        public void AddProject(ResearchProject project) => _repo.Add(project);
        public void UpdateProject(ResearchProject project) => _repo.Update(project);
    }
}
