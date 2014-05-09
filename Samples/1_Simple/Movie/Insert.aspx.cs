
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Samples.Simple;
using Samples.Models;

namespace Samples._1_Simple.Movie
{
    public partial class Insert : System.Web.UI.Page
    {

		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Insert method to insert the entered Movie item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            using (_repo)
            {
                var item = new Samples.Simple.Movie();

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes
                    _repo.Add<Samples.Simple.Movie>(item);
                    _repo.SaveChanges();

                    Response.Redirect("Default.aspx");
                }
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
