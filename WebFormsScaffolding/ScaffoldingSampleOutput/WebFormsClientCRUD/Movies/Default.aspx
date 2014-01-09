<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsClientCRUD.Movies.Default" %>
<%@ Register TagPrefix="user" TagName="Details" Src="Details.ascx" %>
<%@ Register TagPrefix="user" TagName="Update" Src="Update.ascx" %>
<%@ Register TagPrefix="user" TagName="Create" Src="Create.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table class="table table-striped">
        <thead>
            <tr>
                <th></th>
                <th>Title</th>
                <th>Director</th>
                <th>Ticket Price</th>
            </tr>
        </thead>
        <tbody data-bind="foreach: movies">
            <tr>
                <td>
                    <button class="btn btn-default btn-xs" data-bind="click:$root.showMovieDetails">Details</button> 
                </td>
                <td data-bind="text:Title"></td>
                <td data-bind="text:Director"></td>
                <td data-bind="text:TicketPrice"></td>
            </tr>
        </tbody>
    </table>

    <p>
        <button class="btn btn-default" data-bind="click:showMovieDetails">Create New</button>
    </p>

    <div class="modal fade" id="movieModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-bind="showModal: movieModal">
      <div class="modal-dialog">


        <%-- Details Pane --%>
        <user:Details runat="server" />

        <%-- Update Pane --%>
        <user:Update runat="server" />

        <%--  Create Pane  --%>        
        <user:Create runat="server" />


      </div>
    </div>



    <script src="Default.js"></script>
    <script>
        ko.applyBindings(defaultViewModel);
        defaultViewModel.getMovies();
    </script>

</asp:Content>
