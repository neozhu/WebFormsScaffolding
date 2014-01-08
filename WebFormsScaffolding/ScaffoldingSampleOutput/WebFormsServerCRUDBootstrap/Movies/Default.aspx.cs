using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsServerCRUDBootstrap.Models;

namespace WebFormsServerCRUDBootstrap.Movies
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

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public object DetailsForm_GetItem([Control("MoviesList", "SelectedValue")]int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            return repository.Find<Movie>(id);
        }

        public void CreateForm_InsertItem()
        {
            var item = new WebFormsServerCRUDBootstrap.Models.Movie();
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                try
                {
                    repository.Add<Movie>(item);
                    repository.SaveChanges();

                    Response.Redirect("Default.aspx");
                }
                catch (ValidationException valEx)
                {
                    ModelState.AddModelError(String.Empty, valEx.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Could not insert record.");
                }
            }
        }
        

     

    }
}