
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Samples.Associations;
using Samples.Models;

namespace Samples._3_Associations.Category
{
    public partial class Delete : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the Delete methd to delete the selected Category item
        // USAGE: <asp:FormView DeleteMethod="DeleteItem">
        public void DeleteItem(int Id)
        {
            using (_repo)
            {
                var item = _repo.Find<Samples.Associations.Category>(Id);

                if (item != null)
                {
                    _repo.Delete<Samples.Associations.Category>(item);
                    _repo.SaveChanges();
                }
            }
            Response.Redirect("Default.aspx");
        }

        // This is the Select methd to selects a single Category item with the id
        // USAGE: <asp:FormView SelectMethod="GetItem">
        public Samples.Associations.Category GetItem([QueryString]int ? Id)
        {
            if (Id == null)
            {
                return null;
            }

            using (_repo)
            {
                return _repo.Find<Samples.Associations.Category>(Id);
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
