using CC.Web.Dao.Core;
using CC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.Web.Service.Core
{
    public class BaseService<T> : IBaseService<T> where T : class, IEntity
    {
        public IBaseDao<T> curDao { get; set; }

        public void Delete(Guid Id)
        {
            curDao.Delete(Id);
        }

        public void Delete(IEnumerable<Guid> Ids)
        {
            curDao.Delete(Ids);
        }

        public T Find(Guid Id, bool includeDel = false)
        {
            return curDao.Find(Id,includeDel);
        }

        public IQueryable<T> Find(IEnumerable<Guid> Ids, bool includeDel = false)
        {
            return curDao.Find(Ids, includeDel);
        }

        public Guid Insert(T entity)
        {
            return curDao.Insert(entity);
        }

        public IEnumerable<Guid> Insert(IEnumerable<T> entities)
        {
            return curDao.Insert(entities);
        }

        public void Remove(Guid Id)
        {
            curDao.Remove(Id);
        }

        public void Remove(IEnumerable<Guid> Ids)
        {
            curDao.Remove(Ids);
        }

        public void Update(T entity)
        {
            curDao.Update(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            curDao.Update(entity);
        }
    }
}
