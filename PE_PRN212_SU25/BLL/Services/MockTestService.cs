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
    public class MockTestService
    {
        private RepositoryBase<MockTest> _mockTestRepository = new();   

        public List<MockTest> GetAllMockTests()
        {
            return _mockTestRepository.GetAll().Include(p => p.Candidate).ToList();
        }

        public void AddMockTest(MockTest test) => _mockTestRepository.Add(test);
        public void UpdateMockTest(MockTest test) => _mockTestRepository.Update(test);
        public void DeleteMockTest(MockTest test) => _mockTestRepository.Delete(test);

        public List<MockTest> SearchMockTests(string keyword)
        {
            var result = _mockTestRepository.GetAll().Include(p => p.Candidate).AsNoTracking();
            string normalizedKeyword = keyword.Trim().ToLowerInvariant();

            if (keyword.IsNullOrEmpty())
            {
                return result.ToList();
            }

            if (!keyword.IsNullOrEmpty())
            {
                result = result.Where(mt =>
            mt.TestTitle.ToLower().Contains(normalizedKeyword) ||
            mt.SkillArea.ToLower().Contains(normalizedKeyword));
            }

            return result.ToList();
        }
    }
}
