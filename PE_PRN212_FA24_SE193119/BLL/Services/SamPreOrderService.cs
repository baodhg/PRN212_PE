using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class SamPreOrderService
    {
        private RepositoryBase<SamPreOrder> _repo = new();

        public List<SamPreOrder> GetAllProjects()
        {
            return _repo.GetAll().Include(p => p.Product).ToList();
        }

        public List<SamPreOrder> SearchPreOrder(string keyword)
        {
            var result = _repo.GetAll().Include(p => p.Product).AsNoTracking();
            string normalizedKeyword = keyword.Trim().ToLowerInvariant();

            if (keyword.IsNullOrEmpty())
            {
                return result.ToList();
            }

            if (!keyword.IsNullOrEmpty())
            {
                result = result.Where(p =>
                    p.PreOrderNo.ToLower().Contains(normalizedKeyword) ||
                    p.CustomerPhone.ToLower().Contains(normalizedKeyword));
            }
            return result.ToList();
        }

        public void DeletePreOrder(SamPreOrder ord)
        {
            _repo.Delete(ord);
        }

        public void AddOrder(SamPreOrder ord) => _repo.Add(ord);
        public void UpdateOrder(SamPreOrder ord) => _repo.Update(ord);
    }
}
