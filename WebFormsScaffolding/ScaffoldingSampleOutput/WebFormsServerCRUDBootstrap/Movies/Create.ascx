<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Create.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.Movies.Create" %>
<div class="modal-content">
    <div class="modal-header">
        <h4 class="modal-title">Create</h4>
    </div>
    <div class="modal-body">
        <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
        <asp:DynamicControl DataField="Title" Mode="Insert" runat="server" />
        <asp:DynamicControl DataField="Director" Mode="Insert" runat="server" />
        <asp:DynamicControl DataField="TicketPrice" Mode="Insert" runat="server" />
        <asp:DynamicControl DataField="InTheaters" Mode="Insert" runat="server" />

    </div>

    <div class="modal-footer">
        <asp:LinkButton CssClass="btn btn-default" Text="Cancel" CommandName="Cancel" CausesValidation="false" OnClientClick="$('#createModal').modal('hide')" runat="server" />
        <asp:LinkButton CssClass="btn btn-primary" Text="Save" CommandName="Insert" runat="server" />
    </div>
</div>