using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Samples.Validation;
using Samples.Models;

namespace Samples._2_Validation.Books
{
    public partial class Default : System.Web.UI.Page
    {
		protected Samples.Models.ApplicationDbContext _db = new Samples.Models.ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Model binding method to get List of Book entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<Samples.Validation.Book> GetData()
        {
            return _db.Books;
        }
    }
}

