<%@ Control Language="C#" CodeBehind="Default_Insert.ascx.cs" Inherits="$rootnamespace$.Default_InsertEntityTemplate" %>

<%@ Reference Control="~/DynamicData/EntityTemplates/Default.ascx" %>

<asp:EntityTemplate runat="server" ID="EntityTemplate1">
    <ItemTemplate>
           <li class="DDLightHeader" style="list-style-type: none;">
            <asp:Label ID="Label1" runat="server" OnInit="Label_Init" OnPreRender="Label_PreRender" />
        </li>
        <li style="list-style-type: none;">
            <asp:DynamicControl runat="server" ID="DynamicControl" Mode="Insert" OnInit="DynamicControl_Init" />
        </li>
    </ItemTemplate>
</asp:EntityTemplate>

