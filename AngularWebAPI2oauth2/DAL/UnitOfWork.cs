using System;
using System.Data.Entity;

namespace AngularWebAPI2oauth2.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork()
        {
            _context = new ApplicationContext();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public DbContext GetDbContext()
        {
            return _context;
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}