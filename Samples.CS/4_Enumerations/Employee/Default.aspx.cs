
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Samples.Enumerations;
using Samples.CS.Models;

namespace Samples._4_Enumerations.Employee
{
    public partial class Default : System.Web.UI.Page
    {
		protected IGenericRepository _repo = new GenericRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Model binding method to get List of Employee entries
        // USAGE: <asp:ListView SelectMethod="GetData">
        public IQueryable<Samples.Enumerations.Employee> GetData()
        {
            return _repo.Query<Samples.Enumerations.Employee>();
        }
    }
}



