using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebFormsServerCRUD.Models
{
    public class MoviesContext:DbContext
    {
        public MoviesContext():base("name=MoviesContext")
        {
        }

        public IDbSet<Movie> Movies { get; set; }
    }
}