
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
    public partial class Default : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of Product entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<Samples.Associations.Product> GetData()
        {
            return _repo.Query<Samples.Associations.Product>().Include(m => m.Category);
        }
    }
}

