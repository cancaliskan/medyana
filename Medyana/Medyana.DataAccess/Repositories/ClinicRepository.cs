using System.Linq;
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

        public new Clinic GetById(int id)
        {
            return ApplicationContext.Clinics.Include("Equipments").ToList().FirstOrDefault(x => x.Id == id);
        }

        public ApplicationDbContext ApplicationContext => Context as ApplicationDbContext;
    }
}