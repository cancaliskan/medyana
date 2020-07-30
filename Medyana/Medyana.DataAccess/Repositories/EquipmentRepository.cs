using Microsoft.EntityFrameworkCore;

using Medyana.DataAccess.Contracts;
using Medyana.Domain.Entities;

namespace Medyana.DataAccess.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DbContext context) : base(context)
        {
        }
    }
}