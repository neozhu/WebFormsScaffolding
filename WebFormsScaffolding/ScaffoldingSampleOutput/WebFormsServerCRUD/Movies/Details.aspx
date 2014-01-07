<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WebFormsServerCRUD.Movies.Details" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<%-- 
    You can modify the appearance of this page by modifying the HTML markup below or
    by creating custom Dynamic Data field templates.

    *  Use the DynamicDataTemplatesCS NuGet package to install the default 
    Dynamic Data field templates (Install-Package DynamicDataTemplatesCS).
        
    *  Learn more about Dynamic Data at http://www.asp.net/web-forms/videos/aspnet-dynamic-data  
--%>

    <h1>Movie Details</h1>

    <asp:DetailsView
        Id="MoviesDetails"
        ItemType="WebFormsServerCRUD.Models.Movie"
        DataKeyNames="Id"
        SelectMethod="MoviesDetails_GetItem"
        runat="server">  
        <Fields>
            <asp:DynamicField DataField="Id" />
            <asp:DynamicField DataField="Title" />
            <asp:DynamicField DataField="Director" />
            <asp:DynamicField DataField="TicketPrice" HeaderText="Ticket Price" />
        </Fields>             
        <EmptyDataTemplate>
            Sorry, no matching database record found.
        </EmptyDataTemplate>
    </asp:DetailsView>
</asp:Content>
