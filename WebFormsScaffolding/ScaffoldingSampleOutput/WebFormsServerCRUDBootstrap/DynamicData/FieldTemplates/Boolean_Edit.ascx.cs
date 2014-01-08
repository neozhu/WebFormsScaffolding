using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;

namespace WebFormsServerCRUDBootstrap.DynamicData.FieldTemplates
{
    public partial class Boolean_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get form label from model DisplayName attribute
            Label1.Text = Column.DisplayName;

            // Get help text from model Description attribute
            Description.Text = Column.Description;
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object val = FieldValue;
            if (val != null)
                CheckBox1.Checked = (bool)val;
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[Column.Name] = CheckBox1.Checked;
        }

        public override Control DataControl
        {
            get
            {
                return CheckBox1;
            }
        }
    }
}
