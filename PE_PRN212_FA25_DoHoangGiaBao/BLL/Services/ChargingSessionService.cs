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
    public class ChargingSessionService
    {
        private RepositoryBase<ChargingSession> _repo = new();

        public List<ChargingSession> GetAllSession()
        {
            return _repo.GetAll().Include(s => s.Vehicle).ToList();
        }

        public void AddSession(ChargingSession session) => _repo.Add(session);
        public void UpdateSession(ChargingSession test) => _repo.Update(test);
        public void DeleteSession(ChargingSession test) => _repo.Delete(test);

        public List<ChargingSession> SearchSession(string keyword)
        {
            var result = _repo.GetAll().Include(s => s.Vehicle).AsNoTracking();
            string normalizedKeyword = keyword.Trim().ToLowerInvariant();

            if (keyword.IsNullOrEmpty())
            {
                return result.ToList();
            }

            if (!keyword.IsNullOrEmpty())
            {
                result = result.Where(s =>
            s.SessionTitle.ToLower().Contains(normalizedKeyword) ||
            s.ChargingStation.ToLower().Contains(normalizedKeyword));
            }

            return result.ToList();
        }
    }
}
