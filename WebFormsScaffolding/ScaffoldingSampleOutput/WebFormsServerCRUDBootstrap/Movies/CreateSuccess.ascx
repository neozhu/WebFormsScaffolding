<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateSuccess.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.Movies.InsertSuccess" %>
<div class="modal-content">
    <div class="modal-header">
        <h4 class="modal-title">Create</h4>
    </div>
    <div class="modal-body">
     
        <div class="alert alert-success">
            Success! New record created.
        </div>

    </div>

    <div class="modal-footer">
        <a class="btn btn-primary" onclick="$('#createModal').modal('hide')">Close</a>
    </div>
</div>