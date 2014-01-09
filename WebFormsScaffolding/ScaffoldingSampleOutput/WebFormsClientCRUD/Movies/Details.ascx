<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="WebFormsClientCRUD.Movies.Details" %>


<div id="detailsPane" class="modal-content" data-bind="visible: detailsPane">
    <div class="modal-header">
    <button type="button" class="close" data-bind="click:hideMovieModal" aria-hidden="true">&times;</button>
    <h4 class="modal-title" id="myModalLabel">Movie Details</h4>
    </div>

    <div class="modal-body">

        <table class="table table-bordered" data-bind="with:selectedMovie">             
            <tr>
                <td>Title</td>
                <td data-bind="text:Title"></td>
            </tr>
            <tr>
                <td>Director</td>
                <td data-bind="text:Director"></td>
            </tr>
            <tr>
                <td>Ticket Price</td>
                <td data-bind="text:TicketPrice"></td>
            </tr>
        </table>

    </div>
    <div class="modal-footer">
    <button type="button" class="btn btn-default" data-bind="click:hideMovieModal">Close</button>
    <button type="button" class="btn btn-primary" data-bind="click:showMovieUpdate">Edit</button>
    </div>

</div>

