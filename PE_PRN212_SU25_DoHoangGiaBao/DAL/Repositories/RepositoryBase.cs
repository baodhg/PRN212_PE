using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
        public class RepositoryBase<T> where T : class
        {
            private readonly Su25jlptmockTestDbContext _context;
            private readonly Microsoft.EntityFrameworkCore.DbSet<T> _dbSet;

            public RepositoryBase()
            {
                _context = new Su25jlptmockTestDbContext();
                _dbSet = _context.Set<T>();
            }

            public void Add(T entity)
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }

            public void Update(T entity)
            {
                _context.ChangeTracker.Clear();
                _dbSet.Update(entity);
                _context.SaveChanges();
            }

            public void Delete(T entity)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }

            public IQueryable<T> GetAll()
            {
                return _dbSet.AsQueryable().AsNoTracking();
            }

            public T? GetById(object id)
            {
                return _dbSet.Find(id);
            }
        }
}
