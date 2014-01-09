using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebFormsClientCRUD.Models
{
   


        /// <summary>
        /// A generic repository for interacting with a database using the Entity Framework.
        /// This generic repository enables you to execute the following database commands:
        /// 
        /// * Query - Select a set of data from the database. 
        /// * Find - Select a single item from the database.
        /// * Add - Add a single item to the database.
        /// * Remove - Remove a single item from the database.
        /// * SqlQuery - Executes a stored procedure or dynamic SQL.
        /// * SaveChanges - Call this method after calling Add or Remove or modifying an entity to save changes.
        /// 
        /// </summary>
        public class GenericRepository : IGenericRepository, IDisposable
        {
            private DbContext context;

            /// <summary>
            /// You must pass an existing DbContext to the constructor.
            /// </summary>
            /// <example>
            ///     var repository = new GenericRepository(new MoviesDbContext());
            /// </example>
            public GenericRepository(DbContext context)
            {
                this.context = context;
            }

            /// <summary>
            /// Select a set of data from the database
            /// </summary>
            /// <example>
            ///    repository.Query{Movie}() -> Returns all movies
            ///    repository.Query{Movie}(m=>m.Actors) -> Returns all movies and associated actors
            ///    repository.Query{Movie}().Where(m=>m.Actor.Name=="Harrison Ford") -> Returns all movies with Harrison Ford
            /// </example>
            public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
            {
                var query = this.context.Set<T>().AsQueryable();
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
                return query;
            }

            /// <summary>
            /// Select a single item from the database
            /// </summary>
            /// <example>
            ///     repository.Find{Movie}(23) -> Returns movie with a primary key value of 23
            /// </example>
            public T Find<T>(params object[] keyValues) where T : class
            {
                return this.context.Set<T>().Find(keyValues);
            }

            /// <summary>
            /// Adds a new item to the database
            /// </summary>
            /// <example>
            ///     repository.Add{Movie}(movieToAdd) -> Adds a new movie to the database
            /// </example>
            public void Add<T>(T entityToCreate) where T : class
            {
                this.context.Set<T>().Add(entityToCreate);
            }

            /// <summary>
            /// Removes an item from the database
            /// </summary>
            /// <example>
            ///     repository.remove{Movie}(23) -> Removes movie with a primary key value of 23.
            /// </example>

            public void Remove<T>(params object[] keyValues) where T : class
            {
                var entity = this.Find<T>(keyValues);
                this.context.Set<T>().Remove(entity);
            }

            /// <summary>
            /// Save changes to the database. Enables you to execute multiple database commands
            /// in a transaction.
            /// </summary>
            public void SaveChanges()
            {
                try
                {
                    this.context.SaveChanges();
                }
                catch (DbEntityValidationException dbVal)
                {
                    var firstError = dbVal.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage;
                    throw new ValidationException(firstError);
                }
            }


            /// <summary>
            /// Executes a stored procedure or (yuck) dynamic sql
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
