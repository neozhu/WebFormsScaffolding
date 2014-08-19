<%@ Page Title="MovieDelete" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="Samples._1_Simple.Movies.Delete" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Movie?</h3>
        <asp:FormView runat="server"
            ItemType="Samples.Simple.Movie" DataKeyNames="Id"
            DeleteMethod="DeleteItem" SelectMethod="GetItem"
            OnItemCommand="ItemCommand" RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Movie with Id <%: Request.QueryString["Id"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
                <fieldset class="form-horizontal">
                    <legend>Delete Movie</legend>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>Id</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Id" ID="Id" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>Title</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Title" ID="Title" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>Director</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Director" ID="Director" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>Comments</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Comments" ID="Comments" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>Count</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Count" ID="Count" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>Price</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="Price" ID="Price" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong>Release Date</strong>
								</div>
								<div class="col-sm-4">
									<asp:DynamicControl runat="server" DataField="ReleaseDate" ID="ReleaseDate" Mode="ReadOnly" />
								</div>
							</div>
                 	<div class="row">
					  &nbsp;
					</div>
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger" />
							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>

