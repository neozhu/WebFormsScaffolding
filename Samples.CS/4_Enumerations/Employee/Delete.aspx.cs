
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using Samples.Enumerations;
using Samples.CS.Models;

namespace Samples._4_Enumerations.Employee
{
    public partial class Delete : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Delete methd to delete the selected Employee item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(Guid Id)
        {
            using (_repo)
            {
                var item = _repo.Find<Samples.Enumerations.Employee>(Id);

                if (item != null)
                {
                    _repo.Delete<Samples.Enumerations.Employee>(Id);
                    _repo.SaveChanges();
                }
            }
            Response.Redirect("../Default");
        }

        // This is the Select methd to selects a single Employee item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public Samples.Enumerations.Employee GetItem([FriendlyUrlSegmentsAttribute(0)]Guid? Id)
        {
            if (Id == null)
            {
                return null;
            }

            using (_repo)
            {
	            return _repo.Query<Samples.Enumerations.Employee>().Where(m => m.Id == Id).FirstOrDefault();
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



