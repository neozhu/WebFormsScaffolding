<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="WebFormsServerCRUD.Movies.Update" %>


<%-- 
    You can modify the appearance of this page by modifying the HTML markup below or
    by creating custom Dynamic Data field templates.

    *  Use the DynamicDataTemplatesCS NuGet package to install the default 
    Dynamic Data field templates (Install-Package DynamicDataTemplatesCS).
        
    *  Learn more about Dynamic Data at http://www.asp.net/web-forms/videos/aspnet-dynamic-data  
--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Edit Existing Movie</h1>

    <asp:FormView
        Id="MoviesForm"
        DefaultMode="Edit"
        ItemType="WebFormsServerCRUD.Models.Movie" 
        DataKeyNames="Id"
        SelectMethod="MoviesForm_GetItem"          
        UpdateMethod="MoviesForm_UpdateItem"
        runat="server">
        <EditItemTemplate>
            <asp:ValidationSummary  runat="server" />
            <div class="form-group">
                <asp:Label AssociatedControlID="Title" Text="Title" runat="server" />
                <asp:DynamicControl ID="Title" DataField="Title" Mode="Edit" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="Director" Text="Director" runat="server" />
                <asp:DynamicControl ID="Director" DataField="Director"  Mode="Edit" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="TicketPrice" Text="Ticket Price" runat="server" />
                <asp:DynamicControl ID="TicketPrice" DataField="TicketPrice" Mode="Edit" runat="server" />
            </div>
            <asp:Button Text="Edit" CommandName="Update" runat="server" />
        </EditItemTemplate>   
                <EmptyDataTemplate>
            Sorry, no matching database record found.
        </EmptyDataTemplate>                        
    </asp:FormView>

</asp:Content>
