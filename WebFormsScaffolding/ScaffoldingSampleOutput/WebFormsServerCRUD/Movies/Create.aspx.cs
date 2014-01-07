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
    public partial class Create : System.Web.UI.Page
    {
        private IGenericRepository repository = new GenericRepository(new MoviesContext());
 
        /// <summary>
        /// Inserts a new item into the database.
        /// </summary>
        public void MoviesForm_InsertItem()
        {
            var item = new Movie();
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