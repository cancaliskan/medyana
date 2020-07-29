using System.Collections.Generic;

using Medyana.Common.Contracts;
using Medyana.Domain.Entities;

namespace Medyana.Business.Contracts
{
    public interface IClinicService
    {
        Response<Clinic> GetById(int id);
        Response<IEnumerable<Clinic>> GetAll();
        Response<Clinic> Add(Clinic entity);
        Response<Clinic> Update(Clinic entity);
        Response<bool> Remove(int id);
    }
}