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
        public Movie DetailsForm_GetItem([Control("MoviesList", "SelectedValue")]int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            return repository.Find<Movie>(id.Value);
        }


        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        //public Movie CreateForm_GetItem(int? id)
        //{
        //    var it = CreateForm.SelectedValue;

        //    if (!id.HasValue)
        //    {
        //        return null;
        //    }

        //    return repository.Find<Movie>(id.Value);
        //}


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

                    MoviesList.DataBind();

                    CreateFormDiv.Attributes["style"] = "display:none";
                    CreateFormSuccessDiv.Attributes["style"] = "";
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

        // The id parameter name should match the DataKeyNames value set on the control
        public void DetailsForm_UpdateItem(int id)
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

        protected void DetailsForm_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            MoviesList.DataBind();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            CreateFormDiv.Attributes["style"] = "";
            CreateFormSuccessDiv.Attributes["style"] = "display:none";
        }

        // The id parameter name should match the DataKeyNames value set on the control
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