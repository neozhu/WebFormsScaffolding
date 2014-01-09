using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public void MoviesList_DeleteItem(int id)
        {
            try
            {
                repository.Remove<Movie>(id);
                repository.SaveChanges();

                // Ensure that we don't end up on an empty page
                MoviesList.DataBind();
            }
            catch (ValidationException valEx)
            {
                ModelState.AddModelError(String.Empty, valEx.Message);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Could not delete record.");
            }
        }

    
    }
}