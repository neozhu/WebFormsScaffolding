
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Samples.Simple;
using Samples.CS.Models;

namespace Samples._1_Simple.Movie
{
    public partial class Default : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Model binding method to get List of Movie entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<Samples.Simple.Movie> GetData()
        {
            return _repo.Query<Samples.Simple.Movie>();
        }
    }
}



