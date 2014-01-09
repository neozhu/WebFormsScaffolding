<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsClientCRUD.Movies.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table class="table">
        <thead>
            <tr>
                <th>Title</th>
                <th>Director</th>
                <th>Ticket Price</th>
            </tr>
        </thead>
        <tbody data-bind="foreach: movies">
            <tr>
                <td data-bind="text:Title"></td>
                <td data-bind="text:Director"></td>
                <td data-bind="text:TicketPrice"></td>
            </tr>
        </tbody>
    </table>

    <p>
        <button class="btn btn-default" data-bind="click:showCreateModal">Create New</button>
    </p>

    <div class="modal fade" id="createModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-bind="showModal: createModal">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h4 class="modal-title" id="myModalLabel">Create Movie</h4>
          </div>

          <div class="modal-body">
            
            <div class="alert alert-danger" data-bind="visible:movieToCreateValidationErrors().length">
             <ul data-bind="foreach: movieToCreateValidationErrors">
                 <li data-bind="text: errorMessage"></li>
             </ul>
            </div>

             <form class="form-horizontal" role="form">
             
                  <div class="form-group">
                    <label for="Title" class="col-sm-3 control-label">Title</label>
                    <div class="col-sm-9">
                      <input id="Title" data-bind="value:movieToCreate.Title" type="text" class="form-control" placeholder="Title" />
                    </div>
                  </div>
                  <div class="form-group">
                    <label for="Director" class="col-sm-3 control-label">Director</label>
                    <div class="col-sm-9">
                      <input id="Director" data-bind="value:movieToCreate.Director" type="text" class="form-control" placeholder="Director" />
                    </div>
                  </div>
                  <div class="form-group">
                    <label for="title" class="col-sm-3 control-label">Ticket Price</label>
                    <div class="col-sm-9">
                      <input id="Ticket Price" data-bind="value:movieToCreate.TicketPrice" type="text" class="form-control" placeholder="Ticket Price" />
                    </div>
                  </div>

             </form>

          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-bind="click:hideCreateModal">Close</button>
            <button type="button" class="btn btn-primary" data-bind="click:create">Save changes</button>
          </div>


        </div>
      </div>
    </div>



    <script src="Default.js"></script>
    <script>
        ko.applyBindings(defaultViewModel);
        defaultViewModel.getMovies();
    </script>

</asp:Content>
