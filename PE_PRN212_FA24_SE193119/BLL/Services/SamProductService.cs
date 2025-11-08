using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class SamProductService
    {
        private readonly RepositoryBase<SamProduct> _repo = new();
        public List<SamProduct> GetAll()
        {
            return _repo.GetAll().ToList();
        }
    }
}
