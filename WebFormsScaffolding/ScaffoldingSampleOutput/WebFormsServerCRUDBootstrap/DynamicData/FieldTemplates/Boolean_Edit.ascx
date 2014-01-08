<%@ Control Language="C#" CodeBehind="Boolean_Edit.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.DynamicData.FieldTemplates.Boolean_EditField" %>

<div class="checkbox">
    <label>
        <input ID="CheckBox1" type="checkbox" runat="server" />
        <asp:Label ID="Label1" runat="server" />
    </label>
</div>
<asp:Label ID="Description" CssClass="help-block" runat="server" />


