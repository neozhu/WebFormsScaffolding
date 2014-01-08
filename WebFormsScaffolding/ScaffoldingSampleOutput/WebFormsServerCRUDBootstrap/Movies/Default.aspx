<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsServerCRUDBootstrap.Movies.Default" %>
<%@ Register TagPrefix="user" TagName="Create" Src="Create.ascx" %>    
<%@ Register TagPrefix="user" TagName="Details" Src="Details.ascx" %>    
<%@ Register TagPrefix="user" TagName="Update" Src="Update.ascx" %>    

<%-- 
    You can modify the appearance of this page by modifying the HTML markup below, modifying the Bootstrap CSS, or
    by modifying the templates in the \DynamicData\FieldTemplates folder.

    *  Learn more about Bootstrap at http://getbootstrap.com/
        
    *  Learn more about Dynamic Data at http://www.asp.net/web-forms/videos/aspnet-dynamic-data  
--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
  
    <link href="Scaffolding.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script>
        function launch_modal(id) {
            console.log(id);
            // Hide all modals using class if required.
            $('.modal').modal('hide');
            $('#' + id).modal('show');
        }
</script>

    <h1>List Movies</h1>

    <p>
        <a class="btn btn-primary" onclick="launch_modal('createModal');">Create New</a>
    </p>

    <!-- List -->
     <asp:UpdatePanel runat="server">
        <ContentTemplate>

        <asp:ListView
            Id="MoviesList"
            ItemType="WebFormsServerCRUDBootstrap.Models.Movie"
            DataKeyNames="Id"
            SelectMethod="MoviesList_GetData"
            runat="server">
            <LayoutTemplate>
                <table class="table table-striped">
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
                        <asp:LinkButton CssClass="btn btn-default btn-xs" CommandName="Select" Text="Details" OnClientClick="launch_modal('detailsModal');"  runat="server" />
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
    </ContentTemplate>
    </asp:UpdatePanel>


    
    <!-- Modal Dialog for Create form-->
    <div class="modal fade" id="createModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:FormView 
                        ID="CreateForm"
                        RenderOuterTable="false"
                        DefaultMode="Insert"
                        ItemType="WebFormsServerCRUDBootstrap.Models.Movie" 
                        InsertMethod="CreateForm_InsertItem" 
                        runat="server">
                        <InsertItemTemplate>
                            <user:Create runat="server" />
                        </InsertItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
        

    <!-- Modal Dialog for Details and Update forms -->
    <div class="modal fade" id="detailsModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="MoviesList" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:FormView 
                        ID="DetailsForm"
                        ItemType="WebFormsServerCRUDBootstrap.Models.Movie" 
                        SelectMethod="DetailsForm_GetItem" 
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
