
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Samples.Associations;
using Samples.CS.Models;

namespace Samples._3_Associations.Product
{
    public partial class Insert : System.Web.UI.Page
    {

		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Insert method to insert the entered Product item
        // USAGE: <asp:FormView InsertMethod="InsertItem">
        public void InsertItem()
        {
            using (_repo)
            {
                var item = new Samples.Associations.Product();

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes
                    _repo.Add<Samples.Associations.Product>(item);
                    _repo.SaveChanges();

                    Response.Redirect("Default");
                }
            }
        }

        protected void ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Default");
            }
        }
    }
}
