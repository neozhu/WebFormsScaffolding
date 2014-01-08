<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.Movies.Details" %>

                
<div class="modal-content">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title">Details</h4>
    </div>

    <div class="modal-body">
        <table class="table table-bordered">
            <tr>
                <th>Title</th>
                <td><asp:DynamicControl DataField="Title" runat="server" /></td>
            </tr>
            <tr>
                <th>Director</th>
                <td><asp:DynamicControl DataField="Director" runat="server" /></td>
            </tr>
            <tr>
                <th>Ticket Price</th>
                <td><asp:DynamicControl DataField="TicketPrice" runat="server" /></td>
            </tr>
            <tr>
                <th>In Theaters</th>
                <td><asp:DynamicControl DataField="InTheaters" runat="server" /></td>
            </tr>
        </table>
    </div>

    <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <asp:LinkButton CssClass="btn btn-primary" Text="Edit" CommandName="Edit" runat="server" />
    </div>
</div>
