using Data.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.DB.Repositories
{
    public abstract class BaseRepository<T>  where T : BaseEntity
    {
        private DbContext db;
        private DbSet<T> dbSet;

        public BaseRepository()
        {
            db = new MoviesWebAppDbContext();
            dbSet = db.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).ToList();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return dbSet.SingleOrDefault(filter);
        }
        public void Insert(T entity)
        {
            db.Entry(entity).State = EntityState.Added;
            db.SaveChanges();
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }

}
