using System;
using System.Collections.Generic;

namespace HarborControl.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> : IDisposable
    {
        public void Create(TEntity entity);

        public void Delete(TId id);

        public TEntity GetById(TId id);

        public void Update(TEntity entity);

        public IList<TEntity> GetAll();
    }
}