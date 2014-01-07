<%@ Control Language="C#" CodeBehind="Default.ascx.cs" Inherits="$rootnamespace$.DefaultEntityTemplate" %>

<asp:EntityTemplate runat="server" ID="EntityTemplate1">
    <ItemTemplate>
       <li style="list-style-type: none;">
            <asp:Label ID="Label1" runat="server" OnInit="Label_Init" />:
            <asp:DynamicControl ID="DynamicControl1" runat="server" OnInit="DynamicControl_Init" />
        </li>
    </ItemTemplate>
</asp:EntityTemplate>

