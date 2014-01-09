<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delete.ascx.cs" Inherits="WebFormsClientCRUD.Movies.Delete" %>


<div id="detailsPane" class="modal-content" data-bind="visible: deletePane">
    <div class="modal-header">
    <button type="button" class="close" data-bind="click: hideMovieModal" aria-hidden="true">&times;</button>
    <h4 class="modal-title" id="myModalLabel">Delete Movie</h4>
    </div>

    <div class="modal-body">

       <p data-bind="with:selectedMovie">
           Are you sure that you want to delete record <span data-bind="text:Id"></span>?
       </p>

    </div>
    <div class="modal-footer">
    <button type="button" class="btn btn-default" data-bind="click: hideMovieModal">Cancel</button>
    <button type="button" class="btn btn-danger" data-bind="click: deleteMovie">Yes</button>
    </div>

</div>

