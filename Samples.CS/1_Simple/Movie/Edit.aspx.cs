using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using Samples.Simple;
using Samples.CS.Models;
namespace Samples._1_Simple.Movie
{
    public partial class Edit : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This is the Update methd to update the selected Movie item
        // USAGE: <asp:FormView UpdateMethod="UpdateItem">
        public void UpdateItem(int  Id)
        {
            using (_repo)
            {
                var item = _repo.Find<Samples.Simple.Movie>(Id);

                if (item == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Item with id {0} was not found", Id));
                    return;
                }

                TryUpdateModel(item);

                if (ModelState.IsValid)
                {
                    // Save changes here
                    _repo.SaveChanges();
                    Response.Redirect("../Default");
                }
            }
        }

        // This is the Select method to selects a single Movie item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public Samples.Simple.Movie GetItem([FriendlyUrlSegmentsAttribute(0)]int? Id)
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
                Response.Redirect("../Default");
            }
        }
    }
}
