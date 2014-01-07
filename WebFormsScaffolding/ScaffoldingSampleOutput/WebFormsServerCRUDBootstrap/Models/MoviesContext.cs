using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebFormsServerCRUDBootstrap.Models
{
    /// <summary>
    /// Use this class to list each of the models in your application that you want
    /// to track with the Entity Framework. Create an IDbSet property for each model class.
    /// </summary>
    public class MoviesContext : DbContext
    {
        static MoviesContext()
        {
            // Uncomment the following line to drop and recreate the database automatically whenever 
            // your model classes change.

            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MoviesContext>());
        }

        /// <summary>
        /// This DbContext connects to the database using a connection string declared in the web.config file
        /// named MoviesContext.
        /// </summary>
        public MoviesContext()
            : base("name=MoviesContext")
        {
        }

        public IDbSet<Movie> Movies { get; set; }
    }

}