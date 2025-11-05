using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JLPTAccountService
    {
        private RepositoryBase<Jlptaccount> _jlptAccountRepository = new();

        public Jlptaccount? Authenticate(string email, string password)
        {
            return _jlptAccountRepository.GetAll()
                .FirstOrDefault(acc => acc.Email == email && acc.Password == password);
        }

    }
}
