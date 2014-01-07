<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.Movies.Details" %>

                
<div class="modal-content">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title">Details</h4>
    </div>

    <div class="modal-body">
        <asp:DynamicControl DataField="Title" runat="server" />
    </div>

    <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <asp:LinkButton CssClass="btn btn-primary" Text="Edit" CommandName="Edit" runat="server" />
        <button type="button" class="btn btn-danger">Delete</button>
    </div>
</div>
