<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Create.ascx.cs" Inherits="WebFormsClientCRUD.Movies.Create" %>


<div id="detailsPane" class="modal-content" data-bind="visible: createPane">
    <div class="modal-header">
    <button type="button" class="close" data-bind="click: hideMovieModal" aria-hidden="true">&times;</button>
    <h4 class="modal-title" id="myModalLabel">Create Movie</h4>
    </div>

    <div class="modal-body">
            
    <div class="alert alert-danger" data-bind="visible: movieToCreateValidationErrors().length">
        <ul data-bind="foreach: movieToCreateValidationErrors">
            <li data-bind="text: errorMessage"></li>
        </ul>
    </div>

        <form class="form-horizontal" role="form" data-bind="with:movieToCreate">
             
            <div class="form-group">
            <label for="Title" class="col-sm-3 control-label">Title</label>
            <div class="col-sm-9">
                <input id="Title" data-bind="value:Title" type="text" class="form-control" placeholder="Title" />
            </div>
            </div>
            <div class="form-group">
            <label for="Director" class="col-sm-3 control-label">Director</label>
            <div class="col-sm-9">
                <input id="Director" data-bind="value:Director" type="text" class="form-control" placeholder="Director" />
            </div>
            </div>
            <div class="form-group">
            <label for="title" class="col-sm-3 control-label">Ticket Price</label>
            <div class="col-sm-9">
                <input id="Ticket Price" data-bind="value:TicketPrice" type="number" class="form-control" placeholder="Ticket Price" />
            </div>
            </div>

        </form>

    </div>
    <div class="modal-footer">
    <button type="button" class="btn btn-default" data-bind="click: hideMovieModal">Close</button>
    <button type="button" class="btn btn-primary" data-bind="click: createMovie">Save changes</button>
    </div>

</div>
