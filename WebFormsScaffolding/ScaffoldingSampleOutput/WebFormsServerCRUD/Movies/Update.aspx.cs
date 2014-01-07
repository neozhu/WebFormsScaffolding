using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsServerCRUD.Models;

namespace WebFormsServerCRUD.Movies
{
    public partial class Update : System.Web.UI.Page
    {

        private IGenericRepository repository = new GenericRepository(new MoviesContext());

        /// <summary>
        /// Update the existing item in the database.
        /// </summary>
        public void MoviesForm_UpdateItem(int id)
        {
            var item = repository.Find<Movie>(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError(String.Empty, String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here
                try
                {
                    repository.SaveChanges();
                    Response.Redirect("Default.aspx");
                }
                catch (ValidationException valEx)
                {
                    ModelState.AddModelError(String.Empty, valEx.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Could not update record.");
                }
            }
        }

        /// <summary>
        /// Gets the item from the database.
        /// </summary>
        public Movie MoviesForm_GetItem([QueryString]int? id)
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