using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples {
    public partial class Enumeration_EditField : System.Web.DynamicData.FieldTemplateUserControl {
        private Type _enumType;
    
        protected void Page_Init(object sender, EventArgs e) {
			Label1.Text = Column.DisplayName;
            DropDownList1.ToolTip = Column.Description;
    
            if (DropDownList1.Items.Count == 0) {
                if (Mode == DataBoundControlMode.Insert || !Column.IsRequired) {
                    DropDownList1.Items.Add(new ListItem("Select An Option", String.Empty));
                }
                PopulateListControl(DropDownList1);
            }
        }
			
		// show bootstrap has-error
		protected void Page_PreRender(object sender, EventArgs e)
        {
            // if validation error then apply bootstrap has-error CSS class
            var isValid = this.Page.ModelState.IsValidField(Column.Name);
            Div1.Attributes["class"] = isValid ? "form-group" : "form-group has-error";
        }

        protected override void OnDataBinding(EventArgs e) {
            base.OnDataBinding(e);
    
            if (Mode == DataBoundControlMode.Edit && FieldValue != null) {
                string selectedValueString = GetSelectedValueString();
                ListItem item = DropDownList1.Items.FindByValue(selectedValueString);
                if (item != null) {
                    DropDownList1.SelectedValue = selectedValueString;
                }
            }
        }
    
        private Type EnumType {
            get {
                if (_enumType == null) {
                    _enumType = Column.GetEnumType();
                }
                return _enumType;
            }
        }
    
        protected override void ExtractValues(IOrderedDictionary dictionary) {
            string value = DropDownList1.SelectedValue;
            if (value == String.Empty) {
                value = null;
            }
            dictionary[Column.Name] = ConvertEditedValue(value);
        }
    
        public override Control DataControl {
            get {
                return DropDownList1;
            }
        }
    }
}
