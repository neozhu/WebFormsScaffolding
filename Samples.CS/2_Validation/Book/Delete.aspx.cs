
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using Samples.Validation;
using Samples.CS.Models;

namespace Samples._2_Validation.Book
{
    public partial class Delete : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Delete methd to delete the selected Book item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(int Id)
        {
            using (_repo)
            {
                var item = _repo.Find<Samples.Validation.Book>(Id);

                if (item != null)
                {
                    _repo.Delete<Samples.Validation.Book>(Id);
                    _repo.SaveChanges();
                }
            }
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single Book item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public Samples.Validation.Book GetItem([FriendlyUrlSegmentsAttribute(0)]int? Id)
        {
            if (Id == null)
            {
                return null;
            }

            using (_repo)
            {
	            return _repo.Query<Samples.Validation.Book>().Where(m => m.Id == Id).FirstOrDefault();
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



