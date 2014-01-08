<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsServerCRUDBootstrap.Movies.Default" %>
<%@ Register TagPrefix="user" TagName="Create" Src="Create.ascx" %>    
<%@ Register TagPrefix="user" TagName="Details" Src="Details.ascx" %>    
<%@ Register TagPrefix="user" TagName="Update" Src="Update.ascx" %>    
<%@ Register TagPrefix="user" TagName="CreateSuccess" Src="CreateSuccess.ascx" %>    

<%-- 
    You can modify the appearance of this page by modifying the HTML markup below, modifying the Bootstrap CSS, or
    by modifying the templates in the \DynamicData\FieldTemplates folder.

    *  Learn more about Bootstrap at http://getbootstrap.com/
        
    *  Learn more about Dynamic Data at http://www.asp.net/web-forms/videos/aspnet-dynamic-data  
--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />  
    <link href="../Content/Scaffolding-WebForms-Bootstrap.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>List Movies</h1>

    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <p>
                <asp:LinkButton ID="CreateButton" CssClass="btn btn-default" OnClick="AddButton_Click" Text="Create New" OnClientclick="$('#createModal').modal('show')" runat="server" />
            </p>
        </ContentTemplate>        
    </asp:UpdatePanel>


    <!-- List -->
     <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DetailsForm" EventName="ItemUpdated" />
        </Triggers>
        <ContentTemplate>
        <asp:ListView
            Id="MoviesList"
            ItemType="WebFormsServerCRUDBootstrap.Models.Movie"
            DataKeyNames="Id"
            SelectMethod="MoviesList_GetData"
            DeleteMethod="MoviesList_DeleteItem"
            runat="server">
            <LayoutTemplate>
                <table class="table table-striped">
                    <thead>
                    <tr>
                        <th></th>
                        <th><asp:LinkButton Text="Id" CommandName="Sort" CommandArgument="Id" runat="server" /></th>
                        <th><asp:LinkButton Text="Title" CommandName="Sort" CommandArgument="Title" runat="server" /></th>                    
                        <th><asp:LinkButton Text="Director" CommandName="Sort" CommandArgument="Director" runat="server" /></th>                    
                        <th><asp:LinkButton Text="Ticket Price" CommandName="Sort" CommandArgument="TicketPrice" runat="server" /></th>                    
                        <th><asp:LinkButton Text="In Theaters" CommandName="Sort" CommandArgument="InTheaters" runat="server" /></th>                    
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
                        <asp:LinkButton CssClass="btn btn-default btn-xs" CommandName="Select" Text="Details" OnClientClick="$('#detailsModal').modal()"  runat="server" />
                        <asp:LinkButton CssClass="btn btn-danger btn-xs" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Delete this record?')"  runat="server" />                    
                    </td>
                    <td><asp:DynamicControl DataField="Id" runat="server" /></td>
                    <td><asp:DynamicControl DataField="Title" runat="server" /></td>
                    <td><asp:DynamicControl DataField="Director" runat="server" /></td>
                    <td><asp:DynamicControl DataField="TicketPrice" runat="server" /></td>
                    <td><asp:DynamicControl DataField="InTheaters" runat="server" /></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                No data in database.
            </EmptyDataTemplate>
        </asp:ListView>
    </ContentTemplate>
    </asp:UpdatePanel>


     
    <!-- Modal Dialog for Create form-->
    <div class="modal fade" id="createModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div id="CreateFormDiv" runat="server">
                        <asp:FormView 
                            ID="CreateForm"
                            DefaultMode="Insert"
                            DataKeyNames="Id"
                            RenderOuterTable="false"
                            ItemType="WebFormsServerCRUDBootstrap.Models.Movie" 
                            InsertMethod="CreateForm_InsertItem" 
                            runat="server">
                            <ItemTemplate>
                                <user:Details runat="server" />
                            </ItemTemplate>
                            <InsertItemTemplate>
                                <user:Create runat="server" />
                            </InsertItemTemplate>
                        </asp:FormView>
                    </div>
                    <div id="CreateFormSuccessDiv" style="display:none" runat="server">
                        <user:CreateSuccess runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



    <!-- Modal Dialog for Details and Update forms -->
    <div class="modal fade" id="detailsModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">

            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:FormView 
                        ID="DetailsForm"
                        RenderOuterTable="false"
                        ItemType="WebFormsServerCRUDBootstrap.Models.Movie" 
                        DataKeyNames="Id"
                        SelectMethod="DetailsForm_GetItem" 
                        UpdateMethod="DetailsForm_UpdateItem"
                        OnItemUpdated="DetailsForm_ItemUpdated"
                        runat="server">
                        <ItemTemplate>
                            <user:Details runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <user:Update Runat="server" />
                        </EditItemTemplate>
                    </asp:FormView>


                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>


</asp:Content>
