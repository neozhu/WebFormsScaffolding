using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsServerCRUD.Models;

namespace WebFormsServerCRUD.Movies
{
    public partial class Details : System.Web.UI.Page
    {

        private IGenericRepository repository = new GenericRepository(new MoviesContext());

        /// <summary>
        /// Gets item from the database and displays the item in the DetailsView.
        /// </summary>
        public Movie MoviesDetails_GetItem([QueryString]int? id)
        {
            Movie item = null;
            if (id.HasValue)
            {
                item = repository.Find<Movie>(id.Value); 
            }
            return item;
        }
    }
}