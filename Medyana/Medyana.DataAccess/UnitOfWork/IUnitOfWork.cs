using System;

using Medyana.DataAccess.Contracts;

namespace Medyana.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IClinicRepository ClinicRepository { get; }
        IEquipmentRepository EquipmentRepository { get; }

        int Complete();
    }
}