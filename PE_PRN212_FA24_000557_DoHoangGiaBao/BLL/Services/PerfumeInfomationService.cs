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
    public class PerfumeInfomationService
    {
        private readonly RepositoryBase<PerfumeInformation> _repo = new();

        public List<PerfumeInformation> GetAllPerfumes()
        {
            return _repo.GetAll().Include(p => p.ProductionCompany).ToList();
        }

        public List<PerfumeInformation> SearchPerfume(string keyword)
        {
            var result = _repo.GetAll().Include(p => p.ProductionCompany).AsNoTracking();
            string normalizedKeyword = keyword.Trim().ToLowerInvariant();

            if (keyword.IsNullOrEmpty())
            {
                return result.ToList();
            }

            if (!keyword.IsNullOrEmpty())
            {
                result = result.Where(p =>
                    p.Ingredients.ToLower().Contains(normalizedKeyword) ||
                    p.Concentration.ToLower().Contains(normalizedKeyword));
            }
            return result.ToList();
        }

        public void DeletePerfume(PerfumeInformation p)
        {
            _repo.Delete(p);
        }

        public void AddPerfume(PerfumeInformation p) => _repo.Add(p);
        public void UpdatePerfume(PerfumeInformation p) => _repo.Update(p);
    }
}
