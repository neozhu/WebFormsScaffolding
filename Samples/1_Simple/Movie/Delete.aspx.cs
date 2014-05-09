
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Samples.Simple;
using Samples.Models;

namespace Samples._1_Simple.Movie
{
    public partial class Delete : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Delete methd to delete the selected Movie item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(int Id)
        {
            using (_repo)
            {
                var item = _repo.Find<Samples.Simple.Movie>(Id);

                if (item != null)
                {
                    _repo.Delete<Samples.Simple.Movie>(Id);
                    _repo.SaveChanges();
                }
            }
            Response.Redirect("Default.aspx");
        }

        // This is the Select methd to selects a single Movie item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public Samples.Simple.Movie GetItem([QueryString]int ? Id)
        {
            if (Id == null)
            {
                return null;
            }

            using (_repo)
            {
                return _repo.Find<Samples.Simple.Movie>(Id);
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}
