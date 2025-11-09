using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PsaccountService
    {
        private readonly RepositoryBase<Psaccount> _repo = new();
        public Psaccount? Authenticate(string email, string password)
        {
            return _repo.GetAll()
                .FirstOrDefault(acc => acc.EmailAddress == email && acc.Password == password);
        }
    }
}
