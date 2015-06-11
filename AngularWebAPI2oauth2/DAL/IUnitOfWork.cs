using System;
using System.Data.Entity;

namespace AngularWebAPI2oauth2.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        DbContext GetDbContext();
    }
}