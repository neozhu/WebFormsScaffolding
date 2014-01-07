using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsServerCRUD.Models;

namespace WebFormsServerCRUD.Movies
{
    public partial class Default : System.Web.UI.Page
    {
        private IGenericRepository repository = new GenericRepository(new MoviesContext());

        /// <summary>
        /// Gets the data for the ListView from the database.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Movie> MoviesList_GetData()
        {
            return repository.Query<Movie>();
        }

    
    }
}