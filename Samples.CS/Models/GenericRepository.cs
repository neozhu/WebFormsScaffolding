using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Samples.Models;


namespace Samples.CS.Models {

    public class GenericRepository : IGenericRepository {

        private ApplicationDbContext _dataContext = new ApplicationDbContext();

        /// <summary>
        /// Generic query method
        /// </summary>
        public IQueryable<T> Query<T>() where T : class {
            return _dataContext.Set<T>().AsQueryable();
        }


		
        /// <summary>
        /// Non Generic query method
        /// Use model type name instead of model type
        /// </summary>
        public IQueryable Query(string entityTypeName)
        {
            var entityType = Type.GetType(entityTypeName);
            return _dataContext.Set(entityType).AsQueryable();
        }


        /// <summary>
        /// Find row by id
        /// </summary>
        public T Find<T>(params object[] keyValues) where T : class {
            return _dataContext.Set<T>().Find(keyValues);
        }


        /// <summary>
        /// Add new entity
        /// </summary>
        public void Add<T>(T entityToCreate) where T : class {
            _dataContext.Set<T>().Add(entityToCreate);
        }


        public void Delete<T>(params object[] keyValues) where T : class {
            var entity = this.Find<T>(keyValues);
            _dataContext.Set<T>().Remove(entity);
        }


        /// <summary>
        /// Save changes and throw validation exceptions
        /// </summary>
        public void SaveChanges() {
            try {
                _dataContext.SaveChanges();
            } catch (DbEntityValidationException dbVal) {
                var firstError = dbVal.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage;
                throw new ValidationException(firstError);
            }
        }


        /// <summary>
        /// Execute stored procedures and dynamic sql
        /// </summary>
        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters) {
            return this._dataContext.Database.SqlQuery<T>(sql, parameters);
        }

        public void Dispose() {
            _dataContext.Dispose();
        }


    }

	
    /// <summary>
    /// This class promotes the Include() method from the entity framework so it
	/// can be used at a higher layer. You might not want to reference in the Entity Framework
	/// in your presentation layer.
    /// </summary>
    public static class GenericRepositoryExtensions
    {
        public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> queryable, Expression<Func<T, TProperty>> relatedEntity) where T : class {
            return System.Data.Entity.QueryableExtensions.Include<T, TProperty>(queryable, relatedEntity);
        }
    }

    public interface IGenericRepository:IDisposable {
        System.Linq.IQueryable<T> Query<T>() where T : class;
        System.Linq.IQueryable Query(string entityTypeName);
        void Add<T>(T entityToCreate) where T : class;
        T Find<T>(params object[] keyValues) where T : class;
        void Delete<T>(params object[] keyValues) where T : class;
        void SaveChanges();
        System.Collections.Generic.IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
    }

}