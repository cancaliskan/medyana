using System.Collections.Generic;

using Medyana.Common.Contracts;
using Medyana.Domain.Entities;

namespace Medyana.Business.Contracts
{
    public interface IEquipmentService
    {
        Response<Equipment> GetById(int id);
        Response<IEnumerable<Equipment>> GetAll();
        Response<Equipment> Add(Equipment entity);
        Response<Equipment> Update(Equipment entity);
        Response<bool> Remove(int id);
    }
}