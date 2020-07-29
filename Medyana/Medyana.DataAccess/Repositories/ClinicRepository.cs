using Microsoft.EntityFrameworkCore;

using Medyana.DataAccess.Context;
using Medyana.DataAccess.Contracts;
using Medyana.Domain.Entities;

namespace Medyana.DataAccess.Repositories
{
    public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
        public ClinicRepository(DbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationContext => Context as ApplicationDbContext;
    }
}