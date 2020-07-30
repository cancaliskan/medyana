using Medyana.DataAccess.Context;
using Medyana.DataAccess.Contracts;
using Medyana.DataAccess.Repositories;

namespace Medyana.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext context)
        {
            _applicationDbContext = context;
            ClinicRepository = new ClinicRepository(_applicationDbContext);
            EquipmentRepository = new EquipmentRepository(_applicationDbContext);
        }

        public IClinicRepository ClinicRepository { get; }

        public IEquipmentRepository EquipmentRepository { get; }

        public int Complete()
        {
            return _applicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationDbContext?.Dispose();
        }
    }
}