using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebFormsServerCRUD.Models
{
    public class GenericRepository : IGenericRepository, IDisposable
    {
        private DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }


        public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            var query = this.context.Set<T>().AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T Find<T>(params object[] keyValues) where T : class
        {
            return this.context.Set<T>().Find(keyValues);
        }

        public void Add<T>(T entityToCreate) where T : class
        {
            this.context.Set<T>().Add(entityToCreate);
        }


        public void Remove<T>(params object[] keyValues) where T : class
        {
            var entity = this.Find<T>(keyValues);
            this.context.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            try {
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbVal)
            {
                var firstError = dbVal.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage;
                throw new ValidationException(firstError);
            }
        }


        /// <summary>
        /// Execute stored procedures and dynamic sql
        /// </summary>
        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return this.context.Database.SqlQuery<T>(sql, parameters);
        }





        public void Dispose()
        {
            context.Dispose();
        }


    }
}