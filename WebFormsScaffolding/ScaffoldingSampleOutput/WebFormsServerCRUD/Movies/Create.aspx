<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="WebFormsServerCRUD.Movies.Create" %>

<%-- 
    You can modify the appearance of this page by modifying the HTML markup below or
    by creating custom Dynamic Data field templates.

    *  Use the DynamicDataTemplatesCS NuGet package to install the default 
    Dynamic Data field templates (Install-Package DynamicDataTemplatesCS).
        
    *  Learn more about Dynamic Data at http://www.asp.net/web-forms/videos/aspnet-dynamic-data  
--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Create New Movie</h1>

    <asp:FormView
        Id="MoviesForm"
        DefaultMode="Insert"
        ItemType="WebFormsServerCRUD.Models.Movie"           
        InsertMethod="MoviesForm_InsertItem"
        runat="server">
        <InsertItemTemplate>
            <asp:ValidationSummary  runat="server" />
            <div class="form-group">
                <asp:Label AssociatedControlID="Title" Text="Title" runat="server" />
                <asp:DynamicControl ID="Title" DataField="Title" Mode="Insert" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="Director" Text="Director" runat="server" />
                <asp:DynamicControl ID="Director" DataField="Director"  Mode="Insert" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label AssociatedControlID="TicketPrice" Text="Ticket Price" runat="server" />
                <asp:DynamicControl ID="TicketPrice" DataField="TicketPrice" Mode="Insert" runat="server" />
            </div>
            <asp:Button Text="Create" CommandName="Insert" runat="server" />
        </InsertItemTemplate>                           
    </asp:FormView>

</asp:Content>
