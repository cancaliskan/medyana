using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Medyana.DataAccess.Context;
using Medyana.DataAccess.Contracts;
using Medyana.Domain.Entities;

namespace Medyana.DataAccess.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DbContext context) : base(context)
        {
        }

        public new Equipment GetById(int id)
        {
            return ApplicationContext.Equipments.Include(clinic => clinic.Clinic).ToList().FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public new IEnumerable<Equipment> GetAll()
        {
            return ApplicationContext.Equipments.Include(clinic => clinic.Clinic).ToList().Where(x => x.IsActive);
        }

        public ApplicationDbContext ApplicationContext => Context as ApplicationDbContext;
    }
}