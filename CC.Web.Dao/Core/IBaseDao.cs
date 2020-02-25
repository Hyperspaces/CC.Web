using CC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Web.Dao.Core
{
    public interface IBaseDao<T> where T : class, IEntity
    {
        /// <summary>
        /// 插入实例
        /// </summary>
        /// <param name="entity">实例</param>
        /// <returns></returns>
        Guid Insert(T entity);

        /// <summary>
        /// 插入多条实例
        /// </summary>
        /// <param name="entities">实例</param>
        /// <returns></returns>
        IEnumerable<Guid> Insert(IEnumerable<T> entities);

        /// <summary>
        /// 删除实例（软）
        /// </summary>
        /// <param name="Id">唯一标识</param>
        /// <returns></returns>
        void Delete(Guid Id);

        /// <summary>
        /// 删除实例（软）
        /// </summary>
        /// <param name="Ids">唯一标识</param>
        /// <returns></returns>
        void Delete(IEnumerable<Guid> Ids);

        /// <summary>
        /// 删除实例
        /// </summary>
        /// <param name="Id">唯一标识</param>
        /// <returns></returns>
        void Remove(Guid Id);

        /// <summary>
        /// 删除多条实例
        /// </summary>
        /// <param name="Ids">唯一标识</param>
        /// <returns></returns>
        void Remove(IEnumerable<Guid> Ids);

        /// <summary>
        /// 更新实例
        /// </summary>
        /// <param name="entity">实例</param>
        void Update(T entity);

        /// <summary>
        /// 更新实例
        /// </summary>
        /// <param name="entity">实例</param>
        void Update(IEnumerable<T> entity);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="includeDel">是否包含被删实例</param>
        /// <returns></returns>
        T Find(Guid Id, bool includeDel = false);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="includeDel">是否包含被删实例</param>
        /// <returns></returns>
        IQueryable<T> Find(IEnumerable<Guid> Ids, bool includeDel = false);
    }
}
