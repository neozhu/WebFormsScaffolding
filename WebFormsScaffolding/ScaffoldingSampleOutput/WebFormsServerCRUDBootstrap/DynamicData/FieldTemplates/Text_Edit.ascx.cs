using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsServerCRUDBootstrap {
    public partial class Text_EditField : System.Web.DynamicData.FieldTemplateUserControl {
        protected void Page_Load(object sender, EventArgs e) {
            // Get form label from modelDisplayName attribute
            Label1.Text = Column.DisplayName;

            // Set maxlength of TextBox
            if (Column.MaxLength < 20) {
                TextBox1.Columns = Column.MaxLength;
            }

            // Get help text from model Description attribute
            Description.Text = Column.Description;
        }

        protected override void OnPreRender(EventArgs e)
        {
            // if validation error then apply bootstrap has-error CSS class
            var isValid = this.Page.ModelState.IsValidField(Column.Name);
            Div1.Attributes["class"] = isValid ? "form-group" : "form-group has-error";
            
            base.OnPreRender(e);
        }
       


        protected override void OnDataBinding(EventArgs e) {
            base.OnDataBinding(e);
            if(Column.MaxLength > 0) {
                TextBox1.MaxLength = Math.Max(FieldValueEditString.Length, Column.MaxLength);
            }
        }
    
        protected override void ExtractValues(IOrderedDictionary dictionary) {
            dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text);
        }
    
        public override Control DataControl {
            get {
                return TextBox1;
            }
        }
    
    }
}
