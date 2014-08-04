using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Samples.Simple;
using Samples.Models;

namespace Samples._1_Simple.Movies
{
    public partial class Default : System.Web.UI.Page
    {
		protected Samples.Models.ApplicationDbContext _db = new Samples.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of Movie entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<Samples.Simple.Movie> GetData()
        {
            return _db.Movies;
        }
    }
}

