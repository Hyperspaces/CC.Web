using CC.Web.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Web.Dao.Core
{
    public class Repository<T> : IRepository<T> where T : class , IEntity
    {
        private readonly CCDbContext _context;
        private DbSet<T> _entities;

        public Repository(CCDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        public void Delete(Guid Id)
        {
            var entity = Entities.FirstOrDefault(e => e.Id == Id);
            entity.Deleted = true;
            _context.SaveChanges();
        }

        public void Delete(IEnumerable<Guid> Ids)
        {
            var entities = Entities.Where(e => Ids.Contains(e.Id));
            foreach (var entity in entities)
            {
                entity.Deleted = true;
            }

            _context.SaveChanges();
        }

        public T Find(Guid Id)
        {
            return Entities.FirstOrDefault(e => e.Id == Id);
        }

        public IQueryable<T> Find(IEnumerable<Guid> Ids)
        {
            return Entities.Where(e => Ids.Contains(e.Id));
        }

        public Guid Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Guid> Insert(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
            _context.SaveChanges();
            return entities.Select(e => e.Id);
        }

        public void Remove(Guid Id)
        {
            var entity = Entities.FirstOrDefault(e => e.Id == Id);
            if (entity == null)
                throw new ArgumentNullException(nameof(Id));

            Entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(IEnumerable<Guid> Ids)
        {
            var entities = Entities.Where(e => Ids.Contains(e.Id));
            if (entities == null)
                throw new ArgumentNullException(nameof(Ids));

            Entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _context.SaveChanges();
        }
    }
}
