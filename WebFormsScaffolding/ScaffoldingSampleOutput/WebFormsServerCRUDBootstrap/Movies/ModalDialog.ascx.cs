using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsServerCRUDBootstrap.Models;
using System.Web.ModelBinding;


namespace WebFormsServerCRUDBootstrap.Movies
{
    public partial class DetailsDialog : System.Web.UI.UserControl
    {
        private IGenericRepository repository = new GenericRepository(new MoviesContext());


        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public object Unnamed_GetItem([Control("MoviesList", "SelectedValue")]int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            return repository.Find<Movie>(id);
        }
    }
}