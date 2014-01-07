<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsServerCRUD.Movies.Default" %>

<%-- 
    You can modify the appearance of this page by modifying the HTML markup below or
    by creating custom Dynamic Data field templates.

    *  Use the DynamicDataTemplatesCS NuGet package to install the default 
    Dynamic Data field templates (Install-Package DynamicDataTemplatesCS).
        
    *  Learn more about Dynamic Data at http://www.asp.net/web-forms/videos/aspnet-dynamic-data  
--%>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>List Movies</h1>

    <p>
        <a href="Create.aspx">Create New</a>
    </p>
    
    <asp:ListView
        Id="MoviesList"
        ItemType="WebFormsServerCRUD.Models.Movie"
        DataKeyNames="Id"
        SelectMethod="MoviesList_GetData"
        runat="server">
        <LayoutTemplate>
            <table>
                <thead>
                <tr>
                    <th></th>
                    <th><asp:LinkButton Text="Id" CommandName="Sort" CommandArgument="Id" runat="server" /></th>
                    <th><asp:LinkButton Text="Title" CommandName="Sort" CommandArgument="Title" runat="server" /></th>                    
                    <th><asp:LinkButton Text="Director" CommandName="Sort" CommandArgument="Director" runat="server" /></th>                    
                    <th><asp:LinkButton Text="TicketPrice" CommandName="Sort" CommandArgument="TicketPrice" runat="server" /></th>                    
                </tr>
                </thead>
                <tbody>
                    <tr id="ItemPlaceholder" runat="server"></tr>
                </tbody>
            </table>

            <asp:DataPager ID="DataPager1" runat="server">
            <Fields>
                <asp:NumericPagerField />
            </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <a href="Details.aspx?id=<%#:Item.Id %>">Details</a>
                    <a href="Update.aspx?id=<%#:Item.Id %>">Edit</a>
                    <a href="Delete.aspx?id=<%#:Item.Id %>">Delete</a>
                </td>
                <td><asp:DynamicControl DataField="Id" runat="server" /></td>
                <td><asp:DynamicControl DataField="Title" runat="server" /></td>
                <td><asp:DynamicControl DataField="Director" runat="server" /></td>
                <td><asp:DynamicControl DataField="TicketPrice" runat="server" /></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            No data in database.
        </EmptyDataTemplate>
    </asp:ListView>


</asp:Content>
