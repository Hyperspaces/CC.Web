using CC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.Web.Dao.Core
{
    public class BaseDao<T> : IBaseDao<T> where T : class, IEntity
    {
        public IRepository<T> Repository { get; set; }

        /// <inheritdoc />
        public void Delete(Guid Id)
        {
            Repository.Delete(Id);
        }

        /// <inheritdoc />
        public void Delete(IEnumerable<Guid> Ids)
        {
            Repository.Delete(Ids);
        }

        /// <inheritdoc />
        public T Find(Guid Id, bool includeDel = false)
        {
            if (includeDel)
            {
                return Repository.Find(Id);
            }
            else 
            {
                var entity = Repository.Find(Id);
                return entity.Deleted ? null : entity;
            }
        }

        /// <inheritdoc />
        public IQueryable<T> Find(IEnumerable<Guid> Ids, bool includeDel = false)
        {
            if (includeDel)
                return Repository.Find(Ids);
            else
                return Repository.Find(Ids).Where(e => !e.Deleted);
        }

        public Guid Insert(T entity)
        {
            return Repository.Insert(entity);
        }

        public IEnumerable<Guid> Insert(IEnumerable<T> entities)
        {
            return Repository.Insert(entities);
        }

        public void Remove(Guid Id)
        {
            Repository.Remove(Id);
        }

        public void Remove(IEnumerable<Guid> Ids)
        {
            Repository.Remove(Ids);
        }

        public void Update(T entity)
        {
            Repository.Update(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            Repository.Update(entity);
        }
    }
}
