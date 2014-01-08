<%@ Control Language="C#" CodeBehind="Text_Edit.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.Text_EditField" %>


<div class="form-group">
    <asp:Label ID="Label1" AssociatedControlID="TextBox1" runat="server" />

    <asp:TextBox ID="TextBox1" Text='<%# FieldValueEditString %>' CssClass="form-control DDTextBox" runat="server" />

    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="None" Enabled="false" />
    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="None" Enabled="false" />
    <asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="None" />
</div>