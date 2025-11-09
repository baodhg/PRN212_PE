using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductionCompanyService
    {
        private readonly RepositoryBase<ProductionCompany> _repo = new();
        public List<ProductionCompany> GetAll()
        {
            return _repo.GetAll().ToList();
        }
    }
}
