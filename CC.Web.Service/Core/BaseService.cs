using CC.Web.Dao.Core;
using CC.Web.Model;
using CC.Web.Model.Core;
using CC.Web.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Web.Service.Core
{
    public class BaseService<T> : IBaseService<T> where T : class, IEntity
    {
        public IBaseDao<T> CurDao { get; set; }

        public IWorkContext WorkContext { get; set; }

        public void Delete(Guid Id)
        {
            CurDao.Delete(Id);
        }

        public void Delete(IEnumerable<Guid> Ids)
        {
            CurDao.Delete(Ids);
        }

        public T Find(Guid Id, bool includeDel = false)
        {
            return CurDao.Find(Id,includeDel);
        }

        public IQueryable<T> Find(IEnumerable<Guid> Ids, bool includeDel = false)
        {
            return CurDao.Find(Ids, includeDel);
        }

        public Guid Insert(T entity)
        {
            return CurDao.Insert(entity);
        }

        public IEnumerable<Guid> Insert(IEnumerable<T> entities)
        {
            return CurDao.Insert(entities);
        }

        public void Remove(Guid Id)
        {
            CurDao.Remove(Id);
        }

        public void Remove(IEnumerable<Guid> Ids)
        {
            CurDao.Remove(Ids);
        }

        public void Update(T entity)
        {
            CurDao.Update(entity);
        }

        public void Update(IEnumerable<T> entity)
        {
            CurDao.Update(entity);
        }
    }
}
