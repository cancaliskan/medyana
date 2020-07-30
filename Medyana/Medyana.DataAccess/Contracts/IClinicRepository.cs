using System.Collections.Generic;

using Medyana.Domain.Entities;

namespace Medyana.DataAccess.Contracts
{
    public interface IClinicRepository : IRepository<Clinic>
    {
        //Clinic GetClinicWithEquipments(int id);
    }
}