<%@ Control Language="C#" CodeBehind="Text_Edit.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.Text_EditField" %>


<div id="Div1" class="form-group" runat="server">
    <asp:Label ID="Label1" AssociatedControlID="TextBox1" runat="server" />
    <asp:TextBox ID="TextBox1" Text='<%# FieldValueEditString %>' CssClass="form-control DDTextBox" runat="server" />
    <asp:Label ID="Description" CssClass="help-block" runat="server" />
</div>