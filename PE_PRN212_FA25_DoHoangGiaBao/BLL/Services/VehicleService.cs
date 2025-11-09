using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class VehicleService
    {
        private RepositoryBase<ElectricVehicle> _repo = new();

        public List<ElectricVehicle> GetAll()
        {
            return _repo.GetAll().ToList();
        }
    }
}
