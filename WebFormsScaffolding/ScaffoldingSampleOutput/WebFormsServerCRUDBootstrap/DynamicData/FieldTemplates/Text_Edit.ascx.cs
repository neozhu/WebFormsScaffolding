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


            Label1.Text = Column.DisplayName;

            if (Column.MaxLength < 20) {
                TextBox1.Columns = Column.MaxLength;
            }
            TextBox1.ToolTip = Column.Description;
    
            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(RegularExpressionValidator1);
            SetUpValidator(DynamicValidator1);

            // Add bootstrap glyph
            //RequiredFieldValidator1.Text = "<span class='glyphicon glyphicon-warning-sign'></span>";
            RequiredFieldValidator1.Text = "<span class='help-block'>A block of help text that breaks onto a new line and may extend beyond one line.</span>";
 
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
